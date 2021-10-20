using System;

namespace descriptors
{
    public class OSHandle : IDisposable
    {
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);
        public IntPtr handle { get; set; }
        private bool isDisposed;

        public OSHandle(IntPtr handle)
        {
            this.handle = handle;
        }
        ~OSHandle()
        {
            if (!isDisposed && handle != IntPtr.Zero)
            {
                Console.WriteLine("Closed handle: {0}", this.handle);
                CloseHandle(handle);
            }
        }
        public void Dispose()
        {
            if (!isDisposed && handle != IntPtr.Zero)
            {

                GC.SuppressFinalize(this);
                isDisposed = true;
                Console.WriteLine("Closed handle: {0}", this.handle);
                CloseHandle(handle);
            }
        }
    }
}
