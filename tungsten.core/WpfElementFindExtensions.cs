using System;
using System.Collections.Generic;
using System.Linq;

namespace tungsten.core
{
    public static class WpfElementFindExtensions
    {
        public static TElement FindFirstElement<TElement>(this SearchSourceElement parent, params By[] bys)
            where TElement : WpfElement
        {
            // TODO: Throw if null
            // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
            // TODO: Control output verbosity in configuration
            Console.WriteLine("Looking for {0} by <{1}>", parent.GetType().FullName, string.Join("; ", bys.Select(by => by.ToString())));
            return TryFindFirstElement<TElement>(parent, bys);
        }

        private static TElement TryFindFirstElement<TElement>(SearchSourceElement parent, By[] bys)
            where TElement : WpfElement
        {
            // TODO: Max depth
            // TODO: Make a few attempts
            var breadthFirstQueue = new Queue<WpfElement>();
            breadthFirstQueue.EnqueueAll(parent.Children);

            while (breadthFirstQueue.Count > 0)
            {
                var current = breadthFirstQueue.Dequeue();
                var asTElement = current as TElement;
                if (asTElement != null && bys.All(by => by.Matches(asTElement)))
                {
                    return asTElement;
                }

                breadthFirstQueue.EnqueueAll(current.Children);
            }

            return null;
        }
    }
}