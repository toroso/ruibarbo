using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;

namespace ruibarbo.core.Common
{
    public static class Screen
    {
        // TODO? Move these to Win32Api class, and move that away from the Win32 namespace?
        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        const Int32 CURSOR_SHOWING = 0x00000001;

        private static int _uniqueId;

        public static Uri CaptureToFile(string description)
        {
            if (!Configuration.Instance.ScreenshotOnFailedAssertion)
            {
                return null;
            }

            try
            {
                return TryCaptureToFile(description);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Uri TryCaptureToFile(string description)
        {
            using (var bitmap = CaptureScreenAndMouseCursor())
            {
                _uniqueId++;
                string filename = String.Format("ruibarbo_{0}_{1}-{2}.png", DateTime.Now.ToString("yyyyMMddHHmmssffff"), _uniqueId, description);
                bitmap.Save(filename, ImageFormat.Png);
                return new Uri(Path.Combine(Directory.GetCurrentDirectory(), filename));
            }
        }

        private static Bitmap CaptureScreenAndMouseCursor()
        {
            var left = Convert.ToInt32(SystemParameters.VirtualScreenLeft);
            var top = Convert.ToInt32(SystemParameters.VirtualScreenTop);
            var width = Convert.ToInt32(SystemParameters.VirtualScreenWidth);
            var height = Convert.ToInt32(SystemParameters.VirtualScreenHeight);

            var bitmap = new Bitmap(width, height);
            using (var gfx = Graphics.FromImage(bitmap))
            {
                gfx.CopyFromScreen(left, top, 0, 0, bitmap.Size);
                DrawMouseCursor(gfx);
            }
            return bitmap;
        }

        private static void DrawMouseCursor(Graphics gfx)
        {
            CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

            if (GetCursorInfo(out pci))
            {
                if (pci.flags == CURSOR_SHOWING)
                {
                    DrawIcon(gfx.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
                    gfx.ReleaseHdc();
                }
            }
        }
    }
}