using System;
using System.Linq;

namespace tungsten.core.Search
{
    internal class ByBuilder<TElement> : IByBuilder<TElement>
        where TElement : class, ISearchSourceElement
    {
        public By ByExpression(Func<TElement, object> extractFunc, object lookFor)
        {
            return By.Custom(extractFunc, lookFor);
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