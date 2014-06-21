using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public static class SearchSourceElementFindExtensions
    {
        public static TElement FindFirstChild<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            // TODO: Control output verbosity in configuration
            var bysWithClass = bys.AppendByClass<TElement>();
            Console.WriteLine("Find child from {0} by <{1}>", parent.GetType().FullName, bysWithClass .Select(by => by.ToString()).Join("; "));
            var found = TryFindFirstChild<TElement>(parent, bys);
            if (found == null)
            {
                throw ManglaException.FindFailed("child", parent, bys, parent.ControlTreeAsString(6));
            }

            return found;
        }

        private static TElement TryFindFirstChild<TElement>(ISearchSourceElement parent, By[] bys)
            where TElement : class, ISearchSourceElement
        {
            // TODO: Make a few attempts
            var breadthFirstQueue = new Queue<ISearchSourceElement>();
            breadthFirstQueue.EnqueueAll(parent.Children());

            while (breadthFirstQueue.Count > 0)
            {
                var current = breadthFirstQueue.Dequeue();
                var asTElement = current as TElement;
                if (asTElement != null && bys.All(by => by.Matches(asTElement)))
                {
                    asTElement.UpdateFoundBy(bys);
                    return asTElement;
                }

                breadthFirstQueue.EnqueueAll(current.Children());
            }

            return null;
        }

        public static TElement FindFirstAncestor<TElement>(this ISearchSourceElement child, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var bysWithClass = bys.AppendByClass<TElement>().ToArray();
            Console.WriteLine("Find ancestor from {0} by <{1}>", child.GetType().FullName, bysWithClass.Select(by => by.ToString()).Join("; "));
            var found = TryFindFirstAncestor<TElement>(child, bys);
            if (found == null)
            {
                throw ManglaException.FindFailed("ancestor", child, bysWithClass, child.ControlAncestorsAsString());
            }

            return found;
        }

        private static TElement TryFindFirstAncestor<TElement>(ISearchSourceElement child, By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var current = child;
            while (true)
            {
                var parents = current.Parents().ToArray();
                if (!parents.Any())
                {
                    return null;
                }

                var matching = parents.OfType<TElement>().Where(e => bys.All(by => by.Matches(e))).ToArray();
                if (matching.Any())
                {
                    var element = matching.First();
                    element.UpdateFoundBy(bys);
                    return element;
                }

                current = parents.First(); // All have the same underlying FrameworkElement
            }
        }

        private static void UpdateFoundBy<TElement>(this TElement element, By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var asUpdateable = element as IAmFoundByUpdatable;
            if (asUpdateable != null)
            {
                asUpdateable.FoundBy(bys);
            }
        }
    }
}