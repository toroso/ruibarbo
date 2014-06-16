using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public abstract class UntypedWpfElement : ISearchSourceElement
    {
        private readonly ISearchSourceElement _searchParent;
        private By[] _bys;

        protected UntypedWpfElement(ISearchSourceElement searchParent)
        {
            _searchParent = searchParent;
            _bys = new By[] { };
        }

        public abstract string Name { get; }
        public abstract Type Class { get; }

        public abstract IEnumerable<FrameworkElement> NativeChildren { get; }
        public abstract FrameworkElement NativeParent { get; }

        public virtual IEnumerable<By> FoundBys
        {
            get { return _bys; }
        }

        public virtual ISearchSourceElement SearchParent
        {
            get { return _searchParent; }
        }

        public abstract int InstanceId { get; }

        internal TWpfElement FoundBy<TWpfElement>(IEnumerable<By> bys)
            where TWpfElement : UntypedWpfElement
        {
            _bys = bys.Concat(new[] { By.Class(Class) }).ToArray();
            return (TWpfElement)this;
        }
    }
}