using System;

namespace tungsten.core.Search
{
    public class ByBuilder<TElement> : IByBuilder<TElement>
        where TElement : class, ISearchSourceElement
    {
        public By ByExpression(Func<TElement, object> extractFunc, object lookFor)
        {
            return By.Custom(extractFunc, lookFor);
        }
    }
}