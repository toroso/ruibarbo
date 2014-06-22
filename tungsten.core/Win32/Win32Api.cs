using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace tungsten.core.Win32
{
    internal static class Win32Api
    {
        public delegate bool EnumChildWindowsDelegate(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr parentHandle, EnumChildWindowsDelegate callback, IntPtr lParam);

        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out Win32Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static IEnumerable<IntPtr> GetChildWindows(IntPtr hwnd)
        {
            var handles = new List<IntPtr>();
            EnumChildWindows(
                hwnd,
                (h, lParam) =>
                {
                    handles.Add(h);
                    return true;
                },
                IntPtr.Zero);
            return handles;
        }

        public static IEnumerable<IntPtr> GetProcessWindows(int processId)
        {
            var handles = new List<IntPtr>();
            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
            {
                EnumThreadWindows(
                    thread.Id,
                    (h, lParam) =>
                    {
                        handles.Add(h);
                        return true;
                    },
                    IntPtr.Zero);
            }
            return handles;
        }

        public static string GetWindowText(IntPtr hwnd)
        {
            var sb = new StringBuilder(1024); // Can probably be longer, but we don't want more
            return GetWindowText(hwnd, sb, sb.Capacity) == 0
                ? null
                : sb.ToString();
        }

        public static string GetClassName(IntPtr hwnd)
        {
            var sb = new StringBuilder(256); // Maximum class name length = 256
            return GetClassName(hwnd, sb, sb.Capacity) == 0
                ? null
                : sb.ToString();
        }

        public static Rect GetBoundsOnScreen(IntPtr hwnd)
        {
            Win32Rect rect;

            if (!GetWindowRect(hwnd, out rect))
            {
                // TODO: Throw
                return default(Rect);
            }

            return new Rect(new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Bottom));
        }
    }
}