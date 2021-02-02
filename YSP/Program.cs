using System;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using System.Windows.Input;
using System.IO;
using System.Text;

namespace YSP
{
    class Program
    {
        
        static readonly byte[] Noclip_on = { 233, 121, 6, 0, 0 };
        static readonly byte[] Noclip_off = { 106, 20, 139, 203, 255 };
        static int wtf;
        static void Main(string[] args)
        {
             Process proc;
             IntPtr startOffset;
             IntPtr hProc;
            Console.WriteLine("Введите имя процесса без .exe: ");
            string ProcName  = Console.ReadLine();
            for (; ; )
            {
                proc = Process.GetProcessesByName(ProcName).FirstOrDefault<Process>();
                startOffset = proc.MainModule.BaseAddress;
                hProc = Program.OpenProcess(Program.ProcessAccessFlags.All, false, proc.Id);
                WriteProcessMemory(hProc, startOffset + 0x20A23C, Noclip_on, (uint)Noclip_on.Length, out wtf);
            }
        }
        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern IntPtr OpenProcess(Program.ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false, SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            Terminate = 1,
            CreateThread = 2,
            VMOperation = 8,
            VMRead = 16,
            VMWrite = 32,
            DupHandle = 64,
            SetInformation = 512,
            QueryInformation = 1024,
            Synchronize = 1048576,
            All = 2035711
        }
    }
}
