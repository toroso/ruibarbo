using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace tungsten.core
{
    public abstract class SearchSourceElement
    {
        protected Dispatcher Dispatcher { get; private set; }

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
    }
}