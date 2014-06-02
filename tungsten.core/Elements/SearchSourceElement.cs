using System;
using System.Collections.Generic;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public abstract class SearchSourceElement
    {
        private SearchSourceElement Parent { get; set; }

        protected SearchSourceElement(SearchSourceElement parent)
        {
            Parent = parent;
        }

        public abstract string Name { get; }
        public abstract Type Class { get; }
        public abstract IEnumerable<By> SearchConditions { get; }
        public abstract IEnumerable<UntypedWpfElement> Children { get; }

        public IEnumerable<SearchSourceElement> ElementPath
        {
            get
            {
                if (Parent != null)
                {
                    foreach (var ancestor in Parent.ElementPath)
                    {
                        yield return ancestor;
                    }
                }
                yield return this;
            }
        }

        internal UntypedWpfElement CreateWpfElement(FrameworkElement element)
        {
            return ElementFactory.ElementFactory.CreateWpfElement(this, element);
        }
    }
}