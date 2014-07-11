using System;
using System.Linq;
using System.Linq.Expressions;
using tungsten.core.ElementFactory;

namespace tungsten.core.Search
{
    internal class ByBuilder<TElement> : IByBuilder<TElement>
        where TElement : class, ISearchSourceElement
    {
        public By ByExpression(Expression<Func<TElement, object>> extractExp, object lookFor)
        {
            return By.Custom(extractExp, lookFor);
        }
    }

    internal static class ByBuilderExtensions
    {
        public static By[] Build<TElement>(this Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return byBuilders.Select(builder => builder(new ByBuilder<TElement>())).ToArray();
        }
    }
}