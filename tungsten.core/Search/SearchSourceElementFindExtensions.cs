using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public static class SearchSourceElementFindExtensions
    {
        public static TElement FindFirstChild<TElement>(this ISearchSourceElement parent, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return parent.FindFirstChild<TElement>(byBuilders.Build());
        }

        public static TElement FindFirstChild<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            // TODO: Control output verbosity in configuration
            var bysWithClass = bys.AppendByClass<TElement>().ToArray();
            //Console.WriteLine("Find child from {0} by <{1}>", parent.GetType().FullName, bysWithClass .Select(by => by.ToString()).Join("; "));
            var found = parent.TryRepeatedlyToFindFirstChild<TElement>(TimeSpan.FromSeconds(5), bys);
            if (found == null)
            {
                throw ManglaException.FindFailed("child", parent, bysWithClass, parent.ControlTreeAsString(6));
            }

            return found;
        }

        public static TElement TryRepeatedlyToFindFirstChild<TElement>(this ISearchSourceElement parent, TimeSpan maxRetryTime, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return parent.TryRepeatedlyToFindFirstChild<TElement>(maxRetryTime, byBuilders.Build());
        }

        public static TElement TryRepeatedlyToFindFirstChild<TElement>(this ISearchSourceElement parent, TimeSpan maxRetryTime, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            TElement found = null;
            Wait.Until(
                () =>
                    {
                        found = parent.TryOnceToFindFirstChild<TElement>(bys);
                        return found != null;
                    },
                maxRetryTime);

            return found;
        }

        public static TElement TryOnceToFindFirstChild<TElement>(this ISearchSourceElement parent, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return parent.TryOnceToFindFirstChild<TElement>(byBuilders.Build());
        }

        public static TElement TryOnceToFindFirstChild<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var breadthFirstQueue = new Queue<ISearchSourceElement>();
            breadthFirstQueue.EnqueueAll(parent.Children());

            while (breadthFirstQueue.Count > 0)
            {
                var current = breadthFirstQueue.Dequeue();
                var asTElement = current as TElement;
                if (asTElement != null && asTElement.GetType() == typeof(TElement) && bys.All(by => by.Matches(asTElement)))
                {
                    asTElement.UpdateFoundBy(bys);
                    return asTElement;
                }

                breadthFirstQueue.EnqueueAll(current.Children());
            }

            return null;
        }

        public static IEnumerable<TElement> FindAllChildren<TElement>(this ISearchSourceElement parent, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return parent.FindAllChildren<TElement>(byBuilders.Build());
        }

        public static IEnumerable<TElement> FindAllChildren<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var breadthFirstQueue = new Queue<ISearchSourceElement>();
            breadthFirstQueue.EnqueueAll(parent.Children());

            while (breadthFirstQueue.Count > 0)
            {
                var current = breadthFirstQueue.Dequeue();
                var asTElement = current as TElement;
                if (asTElement != null && asTElement.GetType() == typeof(TElement) && bys.All(by => by.Matches(asTElement)))
                {
                    asTElement.UpdateFoundBy(bys);
                    yield return asTElement;
                }

                breadthFirstQueue.EnqueueAll(current.Children());
            }
        }

        public static TElement FindFirstAncestor<TElement>(this ISearchSourceElement child, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return child.FindFirstAncestor<TElement>(byBuilders.Build());
        }

        public static TElement FindFirstAncestor<TElement>(this ISearchSourceElement child, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var bysWithClass = bys.AppendByClass<TElement>().ToArray();
            //Console.WriteLine("Find ancestor from {0} by <{1}>", child.GetType().FullName, bysWithClass.Select(by => by.ToString()).Join("; "));
            var found = child.TryOnceToFindFirstAncestor<TElement>(bys);
            if (found == null)
            {
                throw ManglaException.FindFailed("ancestor", child, bysWithClass, child.ControlAncestorsAsString());
            }

            return found;
        }

        public static TElement TryOnceToFindFirstAncestor<TElement>(this ISearchSourceElement child, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return child.TryOnceToFindFirstAncestor<TElement>(byBuilders.Build());
        }

        public static TElement TryOnceToFindFirstAncestor<TElement>(this ISearchSourceElement child, By[] bys)
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

                var matching = parents
                    .OfType<TElement>()
                    .Where(e => e.GetType() == typeof(TElement))
                    .Where(e => bys.All(by => by.Matches(e)))
                    .ToArray();
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