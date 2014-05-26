using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace tungsten.core
{
    public abstract class SearchSourceElement
    {
        private Dispatcher Dispatcher { get; set; }

        protected SearchSourceElement(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
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

        protected WpfElement ToBeReplacedByWpfElementFactory(FrameworkElement element)
        {
            Type current = element.GetType();
            while (current != null)
            {
                switch (current.FullName)
                {
                    case "System.Windows.Window":
                        return new WpfWindow(Dispatcher, element);
                    case "System.Windows.Controls.Button":
                        return new WpfButton(Dispatcher, element);
                    case "System.Windows.FrameworkElement":
                        return new WpfButton(Dispatcher, element);
                }

                current = current.BaseType;
            }

            // TODO: Possibly let the GetFrameworkElementChildren() method return DependencyObjects and select FrameworkElements here. 
            throw new InvalidOperationException("Factory is broken");
        }
    }
}