using System;
using System.Collections.Generic;

namespace tungsten.core
{
    public interface IWpfElement
    {
        string Name { get; }
        Type Class { get; }
        IEnumerable<WpfElement> Children { get; }
        TRet GetDispatched<TRet>(Func<TRet> func);
        void Click();
    }
}