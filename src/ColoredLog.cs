using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityExternalLogWindow
{
 
    /// <summary>
    /// A Unity log handler that can write colored output to the console.
    /// </summary>
    public class ColoredLog : ILogHandler
    {
        private ILogHandler m_DefaultLogHandler = Debug.unityLogger.logHandler;

        public ColoredLog()
        {
            ConsoleUtility.CreateConsole();
            Debug.unityLogger.logHandler = this;
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            try
            {
                switch (logType)
                {
                    case LogType.Exception:
                    case LogType.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogType.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogType.Assert:
                    case LogType.Log:
                    default:
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine(String.Format(format, args));
            }
            catch (Exception)
            {
                //Just ignore it.
            }

            m_DefaultLogHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
            }
            catch (Exception)
            {
                //Ignore
            }

            m_DefaultLogHandler.LogException(exception, context);
        }
    }
}
