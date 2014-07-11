using System;
using System.Linq.Expressions;
using tungsten.core.ElementFactory;

namespace tungsten.core.Search
{
    public interface IByBuilder<TElement>
        where TElement : class, ISearchSourceElement
    {
        By ByExpression(Expression<Func<TElement, object>> extractExp, object lookFor);
    }
}