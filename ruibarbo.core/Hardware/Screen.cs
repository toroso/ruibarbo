using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using ruibarbo.core.Common;

namespace ruibarbo.core.Hardware
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
            var width = (int)SystemParameters.PrimaryScreenWidth;
            var height = (int)SystemParameters.PrimaryScreenHeight;
            using (var bmpScreenCapture = new Bitmap(width, height))
            {
                using (var gfx = Graphics.FromImage(bmpScreenCapture))
                {
                    gfx.CopyFromScreen(
                        0,
                        0,
                        0, 0,
                        bmpScreenCapture.Size,
                        CopyPixelOperation.SourceCopy);
                }

                _uniqueId++;
                string filename = String.Format("rhubaQ{0}_{1}-{2}.png", DateTime.Now.ToString("yyyyMMddHHmmssffff"), _uniqueId, description);
                bmpScreenCapture.Save(filename, ImageFormat.Png);
                return new Uri(Path.Combine(Directory.GetCurrentDirectory(), filename));
            }
        }
    }
}