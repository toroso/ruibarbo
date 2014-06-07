using System;
using System.Collections.Generic;
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

        internal static ManglaException FindFailed(string soughtRelation, SearchSourceElement sourceElement, IEnumerable<By> bys, string foundAsString)
        {
            // TODO: What if sourceElement does not have a name? What to show?
            var message = string.Format("Find {0} failed, from {1} by <{2}>. Found:\n{3}",
                soughtRelation,
                sourceElement.ControlIdentifier(),
                bys.Select(by => by.ToString()).Join("; "),
                foundAsString);
            return new ManglaException(message);
        }

        internal static ManglaException NotVisible(UntypedWpfElement element)
        {
            // TODO: What if element does not have a name? What to show?
            var message = string.Format("Element is not visible: {0}; path {1}",
                element.ControlIdentifier(),
                element.ElementSearchPath());
            return new ManglaException(message);
        }
    }
}