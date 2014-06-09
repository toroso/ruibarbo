﻿using System;
using System.Collections.Generic;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public abstract class SearchSourceElement
    {
        private SearchSourceElement SearchParent { get; set; }

        protected SearchSourceElement(SearchSourceElement searchParent)
        {
            SearchParent = searchParent;
        }

        public abstract string Name { get; }
        public abstract Type Class { get; }
        public abstract IEnumerable<By> SearchConditions { get; }
        public abstract IEnumerable<UntypedWpfElement> Children { get; }

        public IEnumerable<SearchSourceElement> ElementPath
        {
            get
            {
                if (SearchParent != null)
                {
                    foreach (var ancestor in SearchParent.ElementPath)
                    {
                        yield return ancestor;
                    }
                }
                yield return this;
            }
        }

        internal IEnumerable<UntypedWpfElement> CreateWpfElements(FrameworkElement element)
        {
            return CreateWpfElements(this, element);
        }

        internal IEnumerable<UntypedWpfElement> CreateWpfElements(SearchSourceElement parent, FrameworkElement element)
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, element);
        }
    }
}