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

        public virtual IEnumerable<By> SearchConditions
        {
            get { return _bys; }
        }

        public virtual ISearchSourceElement SearchParent
        {
            get { return _searchParent; }
        }

        /// <summary>
        /// Return a list of possible parents. They all represent the same FrameworkElement, but are encapsulated in different
        /// WpfElements.
        /// TODO: Make into extension method
        /// </summary>
        public abstract IEnumerable<UntypedWpfElement> Parents { get; }

        public abstract int InstanceId { get; }

        internal TWpfElement FoundBy<TWpfElement>(IEnumerable<By> bys)
            where TWpfElement : UntypedWpfElement
        {
            _bys = bys.Concat(new[] { By.Class(Class) }).ToArray();
            return (TWpfElement)this;
        }
    }
}