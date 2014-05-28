using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.ElementFactory;

namespace tungsten.core
{
    public abstract class SearchSourceElement
    {
        private Dispatcher Dispatcher { get; set; }
        private IElementFactory ElementFactory { get; set; }
        private SearchSourceElement Parent { get; set; }

        protected SearchSourceElement(Dispatcher dispatcher, IElementFactory elementFactory, SearchSourceElement parent)
        {
            Dispatcher = dispatcher;
            ElementFactory = elementFactory;
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

        protected WpfElement CreateWpfElement(FrameworkElement element)
        {
            return ElementFactory.CreateWpfElement(Dispatcher, this, element);
        }
    }
}