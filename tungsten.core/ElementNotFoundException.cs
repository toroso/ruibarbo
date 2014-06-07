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
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException()
        {
        }

        public ElementNotFoundException(string soughtRelation, SearchSourceElement sourceElement, IEnumerable<By> bys, string foundAsString)
            : base(MessageFrom(soughtRelation, sourceElement, bys, foundAsString))
        {
        }

        private static string MessageFrom(string soughtRelation, SearchSourceElement sourceElement, IEnumerable<By> bys, string foundAsString)
        {
            // TODO: More information about sourceElement? Name might be null?
            return string.Format("Find {0} failed, from {1} by <{2}>. Found:\n{3}",
                soughtRelation,
                sourceElement.Name,
                bys.Select(by => by.ToString()).Join("; "),
                foundAsString);
        }

        public ElementNotFoundException(string message)
            : base(message)
        {
        }

        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ElementNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}