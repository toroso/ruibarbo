using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using tungsten.core.Input;
using tungsten.core.Search;
using tungsten.core.Utils;
using tungsten.core.Wpf.Base;

namespace tungsten.core
{
    [Serializable]
    public class ManglaException : Exception
    {
        private static int _uniqueId;

        public ManglaException()
        {
        }

        public ManglaException(string message)
            : base(message)
        {
        }

        public ManglaException(string message, Uri screenCapture)
            : base(AppendScreenCaptureTo(message, screenCapture))
        {
        }

        public ManglaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ManglaException(string message, Exception innerException, Uri screenCapture)
            : base(AppendScreenCaptureTo(message, screenCapture), innerException)
        {
        }

        private static string AppendScreenCaptureTo(string message, Uri screenCapture)
        {
            return screenCapture != null
                ? string.Format("{0}\n{1}", message, screenCapture.AbsoluteUri)
                : message;
        }

        protected ManglaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        internal static ManglaException FindFailed(string soughtRelation, ISearchSourceElement sourceElement, IEnumerable<By> bys, string foundAsString)
        {
            // TODO: What if sourceElement does not have a name? What to show?
            Uri screenCapture = CaptureScreen("FindFailed");
            var message = string.Format("Find {0} failed, from {1} ({2}) by <{3}>. Found:\n{4}",
                soughtRelation,
                sourceElement.ControlIdentifier(),
                sourceElement.GetType().Name,
                bys.Select(by => by.ToString()).Join("; "),
                foundAsString);
            return new ManglaException(message, screenCapture);
        }

        internal static ManglaException NotVisible(ISearchSourceElement element)
        {
            // TODO: What if element does not have a name? What to show?
            Uri screenCapture = CaptureScreen("NotVisible");
            var message = string.Format("Element is not visible: {0}; path {1}",
                element.ControlIdentifier(),
                element.ElementSearchPath());
            return new ManglaException(message, screenCapture);
        }

        public static ManglaException NoLongerAvailable(ISearchSourceElement element)
        {
            Uri screenCapture = CaptureScreen("NoLongerAvailable");
            var message = string.Format("Element is no longer available: {0}", element.ElementSearchPath());

            return new ManglaException(message, screenCapture);
        }

        public static ManglaException HardwareFailure(int win32Error)
        {
            Uri screenCapture = CaptureScreen("HardwareFailure");
            return new ManglaException(
                "Some simulated input commands were not sent successfully.",
                new Win32Exception(win32Error),
                screenCapture);
        }

        public static ManglaException NotOpen<TNativeElement>(WpfComboBoxBase<TNativeElement> comboBox)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            Uri screenCapture = CaptureScreen("NotOpen");
            var message = string.Format("ComboBox is not open: {0}", comboBox.ControlIdentifier());
            return new ManglaException(message, screenCapture);
        }

        public static Exception NotFocused(ISearchSourceElement me, IInputElement focusedElement)
        {
            Uri screenCapture = CaptureScreen("NotFocused");
            var meAsString = me.ControlIdentifier();
            var focusedElementAsString = new DefaultControlToStringCreator().ControlToString(focusedElement);
            var message = string.Format("Can't type into control since it does not have keyboard focus. Control: {0}; Focused Element: {1}",
                meAsString,
                focusedElementAsString);
            return new ManglaException(message, screenCapture);
        }

        private static Uri CaptureScreen(string description)
        {
            if (!HardwareConfiguration.ScreenshotOnFailedAssertion)
            {
                return null;
            }

            try
            {
                return TryCaptureScreen(description);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static Uri TryCaptureScreen(string description)
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
                string filename = string.Format("rhuba{0}_{1}-{2}.png", DateTime.Now.ToString("yyyyMMddHHmmssffff"), _uniqueId, description);
                bmpScreenCapture.Save(filename, ImageFormat.Png);
                return new Uri(Path.Combine(Directory.GetCurrentDirectory(), filename));
            }
        }
    }
}