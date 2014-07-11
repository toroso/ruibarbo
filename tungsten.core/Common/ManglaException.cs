using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using tungsten.core.Debug;
using tungsten.core.ElementFactory;
using tungsten.core.Hardware;

namespace tungsten.core.Common
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

        internal static ManglaException FindFailed(string soughtRelation, ISearchSourceElement sourceElement, string byAsString, string foundAsString)
        {
            // TODO: What if sourceElement does not have a name? What to show?
            Uri screenCapture = Screen.CaptureToFile("FindFailed");
            var message = string.Format("Find {0} failed, from {1} ({2}) by <{3}>. Found:\n{4}",
                soughtRelation,
                sourceElement.ControlIdentifier(),
                sourceElement.GetType().Name,
                byAsString,
                foundAsString);
            return new ManglaException(message, screenCapture);
        }

        internal static ManglaException StateFailed<TElement>(TElement element, Expression<Func<TElement, bool>> predicateExp)
            where TElement : ISearchSourceElement
        {
            return StateFailed(element, predicateExp, null);
        }

        internal static ManglaException StateFailed<TElement>(TElement element, Expression<Func<TElement, bool>> predicateExp, string info)
            where TElement : ISearchSourceElement
        {
            Uri screenCapture = Screen.CaptureToFile("StateFailed");
            var infoRow = !string.IsNullOrEmpty(info)
                ? "\n  Info:     " + info
                : string.Empty;
            var message = string.Format("State is other than expected\n  Expr:     {0}\n  Control:  {1}\n  Path:     {2}{3}",
                predicateExp,
                element.ControlIdentifier(),
                element.ElementSearchPath(),
                infoRow);
            return new ManglaException(message, screenCapture);
        }

        internal static ManglaException NoLongerAvailable(ISearchSourceElement element)
        {
            Uri screenCapture = Screen.CaptureToFile("NoLongerAvailable");
            var message = string.Format("Element is no longer available: {0}", element.ElementSearchPath());

            return new ManglaException(message, screenCapture);
        }

        internal static ManglaException HardwareFailure(int win32Error)
        {
            Uri screenCapture = Screen.CaptureToFile("HardwareFailure");
            return new ManglaException(
                "Some simulated input commands were not sent successfully.",
                new Win32Exception(win32Error),
                screenCapture);
        }
    }
}