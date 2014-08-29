using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using ruibarbo.core.Debug;
using ruibarbo.core.ElementFactory;

namespace ruibarbo.core.Common
{
    [Serializable]
    public class RuibarboException : Exception
    {
        public RuibarboException()
        {
        }

        public RuibarboException(string message)
            : base(message)
        {
        }

        public RuibarboException(string message, Uri screenCapture)
            : base(AppendScreenCaptureTo(message, screenCapture))
        {
        }

        public RuibarboException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public RuibarboException(string message, Exception innerException, Uri screenCapture)
            : base(AppendScreenCaptureTo(message, screenCapture), innerException)
        {
        }

        private static string AppendScreenCaptureTo(string message, Uri screenCapture)
        {
            return screenCapture != null
                ? string.Format("{0}\n{1}", message, screenCapture.AbsoluteUri)
                : message;
        }

        protected RuibarboException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static RuibarboException WithMessage(string format, params object[] args)
        {
            var screenCapture = CaptureScreenToFile("WithMessage");
            var message = string.Format(format, args);
            return new RuibarboException(message, screenCapture);
        }

        internal static RuibarboException FindFailed(string soughtRelation, ISearchSourceElement sourceElement, string byAsString, string foundAsString)
        {
            // TODO: What if sourceElement does not have a name? What to show?
            Uri screenCapture = CaptureScreenToFile("FindFailed");
            var message = string.Format("Find {0} failed, from {1} ({2}) by <{3}>. Found:\n{4}",
                soughtRelation,
                sourceElement.ControlIdentifier(),
                sourceElement.GetType().Name,
                byAsString,
                foundAsString);
            return new RuibarboException(message, screenCapture);
        }

        internal static RuibarboException StateFailed<TElement>(TElement element, Expression<Func<TElement, bool>> predicateExp)
            where TElement : ISearchSourceElement
        {
            return StateFailed(element, predicateExp, null);
        }

        internal static RuibarboException StateFailed<TElement>(TElement element, Expression<Func<TElement, bool>> predicateExp, string info)
            where TElement : ISearchSourceElement
        {
            Uri screenCapture = CaptureScreenToFile("StateFailed");
            var infoRow = !string.IsNullOrEmpty(info)
                ? "\n  Info:     " + info
                : string.Empty;
            var message = string.Format("State is other than expected\n  Expr:     {0}\n  Control:  {1}\n  Path:     {2}{3}",
                predicateExp,
                element.ControlIdentifier(),
                element.ElementSearchPath(),
                infoRow);
            return new RuibarboException(message, screenCapture);
        }

        internal static RuibarboException NoLongerAvailable(ISearchSourceElement element)
        {
            Uri screenCapture = CaptureScreenToFile("NoLongerAvailable");
            var message = string.Format("Element is no longer available: {0}", element.ElementSearchPath());

            return new RuibarboException(message, screenCapture);
        }

        internal static RuibarboException HardwareFailure(int win32Error)
        {
            var screenCapture = CaptureScreenToFile("HardwareFailure");
            return new RuibarboException(
                "Some simulated input commands were not sent successfully.",
                new Win32Exception(win32Error),
                screenCapture);
        }

        private static Uri CaptureScreenToFile(string description)
        {
            return Screen.CaptureToFile(description);
        }
    }
}