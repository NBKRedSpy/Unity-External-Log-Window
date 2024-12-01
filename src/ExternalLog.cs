using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityExternalLogWindow
{
    public static class ExternalLog
    {
        /// <summary>
        /// Creates the external log window and adds it to the current Unity Debug Log handler chain.
        /// </summary>
        /// <param name="catchUp">Use the Unity Debugger log file to display any logged information that occurred before the console was attached.</param>
        /// <remarks>If the console is created after data has already been logged by the Unity Debugger, the catchUp parameter 
        /// can be used to read the Unity File log and echo it to the console. This will allow the console to match 
        /// the Unity File log's contents.
        public static void Attach(bool catchUp = true)
        {
            ConsoleUtility.CreateConsole();

            if(catchUp)
            {

                string logPath = Application.consoleLogPath;

                if(!string.IsNullOrEmpty(logPath) )
                {
                    try
                    {

                        using (Stream stream = File.Open(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (Stream outStream = Console.OpenStandardOutput())
                        {
                            stream.CopyTo(outStream);
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nConsole log caught up using '{logPath}'\n");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }

            Debug.unityLogger.logHandler = new ColoredLog();

        }

    }
}
