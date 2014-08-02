using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace ruibarbo.core.Common
{
    public static class Screen
    {
        private static int _uniqueId;

        public static Uri CaptureToFile(string description)
        {
            if (!Configuration.ScreenshotOnFailedAssertion)
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
            var left = Convert.ToInt32(SystemParameters.VirtualScreenLeft);
            var top = Convert.ToInt32(SystemParameters.VirtualScreenTop);
            var width = Convert.ToInt32(SystemParameters.VirtualScreenWidth);
            var height = Convert.ToInt32(SystemParameters.VirtualScreenHeight);
            using (var bitmap = new Bitmap(width, height))
            {
                using (var gfx = Graphics.FromImage(bitmap))
                {
                    gfx.CopyFromScreen(left, top, 0, 0, bitmap.Size);
                }

                _uniqueId++;
                string filename = String.Format("rhubaQ{0}_{1}-{2}.png", DateTime.Now.ToString("yyyyMMddHHmmssffff"), _uniqueId, description);
                bitmap.Save(filename, ImageFormat.Png);
                return new Uri(Path.Combine(Directory.GetCurrentDirectory(), filename));
            }
        }
    }
}