using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Elements;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public static class SearchSourceElementFindExtensions
    {
        public static TElement FindFirstChild<TElement>(this SearchSourceElement parent, params By[] bys)
            where TElement : UntypedWpfElement
        {
            // TODO: Control output verbosity in configuration
            // TODO: Include Class in bys. Try to reuse from WpfElement.FoundBy() and WpfElementExtensions.ElementSearchPath().
            Console.WriteLine("Find child from {0} by <{1}>", parent.GetType().FullName, bys.Select(by => by.ToString()).Join("; "));
            var found = TryFindFirstChild<TElement>(parent, bys);
            if (found == null)
            {
                throw new ElementNotFoundException("child", parent, bys, parent.ControlTreeAsString(6));
            }

            return found;
        }

        private static TElement TryFindFirstChild<TElement>(SearchSourceElement parent, By[] bys)
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
                throw new ElementNotFoundException("ancestor", child, bys, child.ElementNameOrClassPath());
            }

            return found;
        }

        private static TElement TryFindFirstAncestor<TElement>(UntypedWpfElement child, By[] bys)
            where TElement : UntypedWpfElement
        {
            var current = child;
            while (true)
            {
                current = current.Parent;
                if (current == null)
                {
                    return null;
                }

                var asTElement = current as TElement;
                if (asTElement != null && bys.All(by => by.Matches(asTElement)))
                {
                    return asTElement.FoundBy<TElement>(bys);
                }
            }
        }
    }
}