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
                sourceElement.Name,
                bys.Select(by => by.ToString()).Join("; "),
                foundAsString);
            return new ManglaException(message);
        }

        internal static ManglaException NotVisible(UntypedWpfElement element, UntypedWpfElement parent)
        {
            // TODO: What if element or parent does not have a name? What to show?
            // TODO: Include bys?
            var message = string.Format("Element is not visible: {0}; parent: {1}", element.Name, parent.Name);
            return new ManglaException(message);
        }
    }
}