using System;
using System.Runtime.InteropServices;

namespace descriptors
{
    class Program
    {
        public const int STD_OUTPUT_HANDLE = -11;
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        static void Main(string[] args)
        {
            OSHandle handle = new OSHandle(GetStdHandle(STD_OUTPUT_HANDLE));
            handle.Dispose();
        }
    }
}
