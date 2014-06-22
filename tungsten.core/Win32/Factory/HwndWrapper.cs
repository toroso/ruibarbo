using System;

namespace tungsten.core.Win32.Factory
{
    internal class HwndWrapper
    {
        public IntPtr Hwnd { get; private set; }

        public HwndWrapper(IntPtr hwnd)
        {
            Hwnd = hwnd;
        }
    }
}