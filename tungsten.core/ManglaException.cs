using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using tungsten.core.Hardware;
using tungsten.core.Search;
using tungsten.core.Utils;
using tungsten.core.Wpf.Base;

namespace tungsten.core
{
    [Serializable]
    public class ManglaException : Exception
    {
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
            Uri screenCapture = Screen.CaptureToFile("FindFailed");
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
            Uri screenCapture = Screen.CaptureToFile("NotVisible");
            var message = string.Format("Element is not visible: {0}; path {1}",
                element.ControlIdentifier(),
                element.ElementSearchPath());
            return new ManglaException(message, screenCapture);
        }

        public static ManglaException NoLongerAvailable(ISearchSourceElement element)
        {
            Uri screenCapture = Screen.CaptureToFile("NoLongerAvailable");
            var message = string.Format("Element is no longer available: {0}", element.ElementSearchPath());

            return new ManglaException(message, screenCapture);
        }

        public static ManglaException HardwareFailure(int win32Error)
        {
            Uri screenCapture = Screen.CaptureToFile("HardwareFailure");
            return new ManglaException(
                "Some simulated input commands were not sent successfully.",
                new Win32Exception(win32Error),
                screenCapture);
        }

        public static ManglaException NotOpen<TNativeElement>(WpfComboBoxBase<TNativeElement> comboBox)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            Uri screenCapture = Screen.CaptureToFile("NotOpen");
            var message = string.Format("ComboBox is not open: {0}", comboBox.ControlIdentifier());
            return new ManglaException(message, screenCapture);
        }

        public static Exception NotFocused(ISearchSourceElement me, IInputElement focusedElement)
        {
            Uri screenCapture = Screen.CaptureToFile("NotFocused");
            var meAsString = me.ControlIdentifier();
            var focusedElementAsString = focusedElement != null
                ? new DefaultControlToStringCreator().ControlToString(focusedElement)
                : "<null>";
            var message = string.Format("Can't type into control since it does not have keyboard focus. Control: {0}; Focused Element: {1}",
                meAsString,
                focusedElementAsString);
            return new ManglaException(message, screenCapture);
        }
    }
}