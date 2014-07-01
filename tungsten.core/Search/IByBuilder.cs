using System;

namespace tungsten.core.Search
{
    public interface IByBuilder<out TElement>
        where TElement : class, ISearchSourceElement
    {
        By ByExpression(Func<TElement, object> extractFunc, object lookFor);
    }
}