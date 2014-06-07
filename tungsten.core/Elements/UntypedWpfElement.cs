using System.Collections.Generic;
using System.Linq;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public abstract class UntypedWpfElement : SearchSourceElement
    {
        private By[] _bys;

        protected UntypedWpfElement(SearchSourceElement searchParent)
            : base(searchParent)
        {
            _bys = new By[] { };
        }

        public override IEnumerable<By> SearchConditions
        {
            get { return _bys; }
        }

        public abstract UntypedWpfElement Parent { get; }

        internal TWpfElement FoundBy<TWpfElement>(IEnumerable<By> bys)
            where TWpfElement : UntypedWpfElement
        {
            _bys = bys.Concat(new[] { By.Class(Class) }).ToArray();
            return (TWpfElement)this;
        }
    }
}