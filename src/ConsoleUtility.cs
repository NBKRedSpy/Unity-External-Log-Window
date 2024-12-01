using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UnityExternalLogWindow
{
    /// <summary>
    /// Attaches to the application's default console.
    /// </summary>
    /// <remarks>
    /// This code is from StackOverflow user wischi:
    /// https://stackoverflow.com/a/45282476/20226690
    /// </remarks>
    internal static class ConsoleUtility
    {
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            uint lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            uint hTemplateFile);

        private const int MY_CODE_PAGE = 437;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint FILE_SHARE_WRITE = 0x2;
        private const uint OPEN_EXISTING = 0x3;

        private static bool _inited = false;
        public static void CreateConsole()
        {
            //Only using this library to attach to the default console, 
            //  so only init once.  Avoids needing to clean up handles.
            if (_inited) return;

            _inited = true;

            AllocConsole();

            IntPtr stdHandle = CreateFile(
                "CONOUT$",
                GENERIC_WRITE,
                FILE_SHARE_WRITE,
                0, OPEN_EXISTING, 0, 0
            );

            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            StreamWriter standardOutput = new StreamWriter(fileStream);

            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }
    }
}
