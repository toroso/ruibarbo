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

        protected SearchSourceElement(Dispatcher dispatcher, IElementFactory elementFactory)
        {
            Dispatcher = dispatcher;
            ElementFactory = elementFactory;
        }

        public abstract IEnumerable<WpfElement> Children { get; }

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
            return ElementFactory.CreateWpfElement(Dispatcher, element);
        }
    }
}