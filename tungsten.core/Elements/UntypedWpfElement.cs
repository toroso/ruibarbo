using System.Collections.Generic;
using System.Linq;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public abstract class UntypedWpfElement : SearchSourceElement
    {
        private By[] _bys;

        protected UntypedWpfElement(SearchSourceElement parent)
            : base(parent)
        {
            _bys = new By[] { };
        }

        public override IEnumerable<By> SearchConditions
        {
            get { return _bys; }
        }

        internal TWpfElement FoundBy<TWpfElement>(IEnumerable<By> bys)
            where TWpfElement : UntypedWpfElement
        {
            _bys = bys.Concat(new[] { By.Class(GetType()) }).ToArray();
            return (TWpfElement)this;
        }
    }
}