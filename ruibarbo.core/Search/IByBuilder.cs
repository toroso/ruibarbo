using System;
using System.Linq.Expressions;
using ruibarbo.core.ElementFactory;

namespace ruibarbo.core.Search
{
    public interface IByBuilder<TElement>
        where TElement : class, ISearchSourceElement
    {
        By ByExpression(Expression<Func<TElement, object>> extractExp, object lookFor);
    }
}