using System;

namespace ruibarbo.core.Win32.Factory
{
    // Not to be confused with the Microsoft.VisualStudio.PlatformUI.HwndWrapper class
    internal sealed class HwndWrapper
    {
        public IntPtr Hwnd { get; private set; }

        public HwndWrapper(IntPtr hwnd)
        {
            Hwnd = hwnd;
        }
    }
}