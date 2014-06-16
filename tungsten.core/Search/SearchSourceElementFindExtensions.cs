using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Elements;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public static class SearchSourceElementFindExtensions
    {
        public static TElement FindFirstChild<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : UntypedWpfElement
        {
            // TODO: Control output verbosity in configuration
            // TODO: Include Class in bys. Try to reuse from WpfFrameworkElementBase.FoundBy() and WpfElementExtensions.ElementSearchPath().
            Console.WriteLine("Find child from {0} by <{1}>", parent.GetType().FullName, bys.Select(by => by.ToString()).Join("; "));
            var found = TryFindFirstChild<TElement>(parent, bys);
            if (found == null)
            {
                throw ManglaException.FindFailed("child", parent, bys, parent.ControlTreeAsString(6));
            }

            return found;
        }

        private static TElement TryFindFirstChild<TElement>(ISearchSourceElement parent, By[] bys)
            where TElement : UntypedWpfElement
        {
            // TODO: Make a few attempts
            var breadthFirstQueue = new Queue<UntypedWpfElement>();
            breadthFirstQueue.EnqueueAll(parent.Children);

            while (breadthFirstQueue.Count > 0)
            {
                var current = breadthFirstQueue.Dequeue();
                var asTElement = current as TElement;
                if (asTElement != null && bys.All(by => by.Matches(asTElement)))
                {
                    return asTElement.FoundBy<TElement>(bys);
                }

                breadthFirstQueue.EnqueueAll(current.Children);
            }

            return null;
        }

        public static TElement FindFirstAncestor<TElement>(this UntypedWpfElement child, params By[] bys)
            where TElement : UntypedWpfElement
        {
            Console.WriteLine("Find ancestor from {0} by <{1}>", child.GetType().FullName, bys.Select(by => by.ToString()).Join("; "));
            var found = TryFindFirstAncestor<TElement>(child, bys);
            if (found == null)
            {
                throw ManglaException.FindFailed("ancestor", child, bys, child.ControlIdentifierPath());
            }

            return found;
        }

        private static TElement TryFindFirstAncestor<TElement>(UntypedWpfElement child, By[] bys)
            where TElement : UntypedWpfElement
        {
            var current = child;
            while (true)
            {
                var parents = current.Parents.ToArray();
                if (!parents.Any())
                {
                    return null;
                }

                var matching = parents.OfType<TElement>().Where(e => bys.All(by => by.Matches(e))).ToArray();
                if (matching.Any())
                {
                    return matching.First().FoundBy<TElement>(bys);
                }

                current = parents.First(); // All have the same underlying FrameworkElement
            }
        }
    }
}