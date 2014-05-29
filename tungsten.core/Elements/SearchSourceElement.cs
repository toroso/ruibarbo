using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public abstract class SearchSourceElement
    {
        private Dispatcher Dispatcher { get; set; }
        private SearchSourceElement Parent { get; set; }

        protected SearchSourceElement(Dispatcher dispatcher, SearchSourceElement parent)
        {
            Dispatcher = dispatcher;
            Parent = parent;
        }

        public abstract string Name { get; }
        public abstract Type Class { get; }
        public abstract IEnumerable<By> SearchConditions { get; }
        public abstract IEnumerable<WpfElement> Children { get; }

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

        public virtual TRet GetDispatched<TRet>(Func<TRet> func)
        {
            TRet ret = default(TRet);
            Dispatcher.Invoke(() =>
                {
                    ret = func();
                });
            return ret;
        }

        internal WpfElement CreateWpfElement(FrameworkElement element)
        {
            return ElementFactory.ElementFactory.CreateWpfElement(Dispatcher, this, element);
        }
    }
}