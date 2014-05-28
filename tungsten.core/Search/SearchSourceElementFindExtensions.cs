using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Elements;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public static class SearchSourceElementFindExtensions
    {
        public static TElement FindFirstElement<TElement>(this SearchSourceElement parent, params By[] bys)
            where TElement : WpfElement
        {
            // TODO: Control output verbosity in configuration
            // TODO: Include Class in bys. Try to reuse from WpfElement.FoundBy() and WpfElementExtensions.ElementSearchPath().
            Console.WriteLine("Search from {0} by <{1}>", parent.GetType().FullName, bys.Select(by => by.ToString()).Join("; "));
            var found = TryFindFirstElement<TElement>(parent, bys);
            if (found == null)
            {
                // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
                // TODO: Better error message. Include a lot of information about parent and a tree with information about all children.
                throw new Exception(string.Format("Search failed, from {0} by <{1}>", parent.Name, bys.Select(by => by.ToString()).Join("; ")));
            }

            return found;
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
                    return (TElement)asTElement.FoundBy(bys);
                }

                breadthFirstQueue.EnqueueAll(current.Children);
            }

            return null;
        }
    }
}