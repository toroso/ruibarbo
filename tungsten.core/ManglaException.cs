using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.core.Utils;

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

        public ManglaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ManglaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        internal static ManglaException FindFailed(string soughtRelation, ISearchSourceElement sourceElement, IEnumerable<By> bys, string foundAsString)
        {
            // TODO: What if sourceElement does not have a name? What to show?
            var message = string.Format("Find {0} failed, from {1} ({2}) by <{3}>. Found:\n{4}",
                soughtRelation,
                sourceElement.ControlIdentifier(),
                sourceElement.GetType().Name,
                bys.Select(by => by.ToString()).Join("; "),
                foundAsString);
            return new ManglaException(message);
        }

        internal static ManglaException NotVisible(ISearchSourceElement element)
        {
            // TODO: What if element does not have a name? What to show?
            var message = string.Format("Element is not visible: {0}; path {1}",
                element.ControlIdentifier(),
                element.ElementSearchPath());
            return new ManglaException(message);
        }

        public static ManglaException NoLongerAvailable(ISearchSourceElement element)
        {
            var message = string.Format("Element is no longer available: {0}", element.ElementSearchPath());
            return new ManglaException(message);
        }

        public static ManglaException HardwareFailure(int win32Error)
        {
            return new ManglaException("Some simulated input commands were not sent successfully.", new Win32Exception(win32Error));
        }

        public static ManglaException NotOpen<TNativeElement>(WpfComboBoxBase<TNativeElement> comboBox)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            var message = string.Format("ComboBox is not open: {0}", comboBox.ControlIdentifier());
            return new ManglaException(message);
        }
    }
}