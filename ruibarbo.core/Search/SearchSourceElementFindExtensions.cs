using System;
using System.Collections.Generic;
using System.Linq;
using ruibarbo.core.Common;
using ruibarbo.core.Debug;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Utils;

namespace ruibarbo.core.Search
{
    public static class SearchSourceElementFindExtensions
    {
        // TODO: Make configurable. And perhaps overridable in method API.
        private const int MaxSearchDepth = 20;

        public static TElement FindFirstChild<TElement>(this ISearchSourceElement parent)
            where TElement : class, ISearchSourceElement
        {
            return parent.FindFirstChild<TElement>(By.Empty);
        }

        public static TElement FindFirstChild<TElement>(this ISearchSourceElement parent, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return parent.FindFirstChild<TElement>(byBuilders.Build());
        }

        public static TElement FindFirstChild<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            // TODO: Control output verbosity in configuration
            //var bysWithClass = bys.AppendByClass<TElement>();
            //Console.WriteLine("Find child from {0} by <{1}>", parent.GetType().FullName, bysWithClass.Select(by => by.ToString()).Join("; "));
            var found = parent.TryRepeatedlyToFindFirstChild<TElement>(bys);
            if (found == null)
            {
                var controlToStringCreator = new ByControlToStringCreator<TElement>(bys.RemoveByName().ToArray());
                string byAsString = bys
                    .AppendByClass<TElement>()
                    .Select(by => by.ToString())
                    .Join("; ");
                throw RuibarboException.FindFailed("child", parent, byAsString, parent.ControlTreeAsString(controlToStringCreator, 8));
            }

            return found;
        }

        public static TElement TryRepeatedlyToFindFirstChild<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            return Wait.UntilNotNull(() => parent.TryOnceToFindFirstChild<TElement>(bys));
        }

        public static TElement TryOnceToFindFirstChild<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var breadthFirstQueue = new Queue<ElementAndDepth>();
            breadthFirstQueue.EnqueueAll(parent.NativeChildren.Select(c => new ElementAndDepth(c, parent, 0)));

            while (breadthFirstQueue.Count > 0)
            {
                var current = breadthFirstQueue.Dequeue();
                ISearchSourceElement nextParent = null;
                foreach (var element in ElementFactory.ElementFactory.CreateElements(current.Parent, current.NativeElement))
                {
                    nextParent = element;
                    var asTElement = element as TElement;
                    if (asTElement != null && asTElement.GetType() == typeof(TElement) && bys.All(by => by.Matches(asTElement)))
                    {
                        asTElement.UpdateFoundBy(bys);
                        return asTElement;
                    }
                }

                if (current.Depth < MaxSearchDepth && nextParent != null)
                {
                    breadthFirstQueue.EnqueueAll(nextParent.NativeChildren.Select(c => new ElementAndDepth(c, nextParent, current.Depth + 1)));
                }
            }

            return null;
        }

        public static IEnumerable<TElement> FindAllChildren<TElement>(this ISearchSourceElement parent)
            where TElement : class, ISearchSourceElement
        {
            return parent.FindAllChildren<TElement>(By.Empty);
        }

        public static IEnumerable<TElement> FindAllChildren<TElement>(this ISearchSourceElement parent, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return parent.FindAllChildren<TElement>(byBuilders.Build());
        }

        public static IEnumerable<TElement> FindAllChildren<TElement>(this ISearchSourceElement parent, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            var breadthFirstQueue = new Queue<ElementAndDepth>();
            breadthFirstQueue.EnqueueAll(parent.NativeChildren.Select(c => new ElementAndDepth(c, parent, 0)));

            while (breadthFirstQueue.Count > 0)
            {
                var current = breadthFirstQueue.Dequeue();
                ISearchSourceElement nextParent = null;
                foreach (var element in ElementFactory.ElementFactory.CreateElements(current.Parent, current.NativeElement))
                {
                    nextParent = element;

                    var asTElement = element as TElement;
                    if (asTElement != null && asTElement.GetType() == typeof(TElement) && bys.All(by => by.Matches(asTElement)))
                    {
                        asTElement.UpdateFoundBy(bys);
                        yield return asTElement;
                    }
                }

                if (current.Depth < MaxSearchDepth && nextParent != null)
                {
                    breadthFirstQueue.EnqueueAll(nextParent.NativeChildren.Select(c => new ElementAndDepth(c, nextParent, current.Depth + 1)));
                }
            }
        }

        public static TElement FindFirstAncestor<TElement>(this ISearchSourceElement child)
            where TElement : class, ISearchSourceElement
        {
            return child.FindFirstAncestor<TElement>(By.Empty);
        }

        public static TElement FindFirstAncestor<TElement>(this ISearchSourceElement child, params Func<IByBuilder<TElement>, By>[] byBuilders)
            where TElement : class, ISearchSourceElement
        {
            return child.FindFirstAncestor<TElement>(byBuilders.Build());
        }

        public static TElement FindFirstAncestor<TElement>(this ISearchSourceElement child, params By[] bys)
            where TElement : class, ISearchSourceElement
        {
            //Console.WriteLine("Find ancestor from {0} by <{1}>", child.GetType().FullName, bysWithClass.Select(by => by.ToString()).Join("; "));
            var found = child.TryOnceToFindFirstAncestor<TElement>(bys);
            if (found == null)
            {
                var controlToStringCreator = new ByControlToStringCreator<TElement>(bys.RemoveByName().ToArray());
                string byAsString = bys
                    .AppendByClass<TElement>()
                    .Select(by => by.ToString())
                    .Join("; ");
                throw RuibarboException.FindFailed("ancestor", child, byAsString, child.ControlAncestorsAsString(controlToStringCreator));
            }

            return found;
        }

        public static TElement TryOnceToFindFirstAncestor<TElement>(this ISearchSourceElement child)
            where TElement : class, ISearchSourceElement
        {
            return child.TryOnceToFindFirstAncestor<TElement>(By.Empty);
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

        /// <summary>
        /// Return a list of possible parents. They all represent the same FrameworkElement, but are wrapped in different
        /// WpfElements.
        /// </summary>
        private static IEnumerable<ISearchSourceElement> Parents(this ISearchSourceElement me)
        {
            var nativeParent = me.NativeParent;
            return nativeParent != null
                ? ElementFactory.ElementFactory.CreateElements(null, nativeParent)
                : new ISearchSourceElement[] { };
        }

        private static void UpdateFoundBy<TElement>(this TElement element, IEnumerable<By> bys)
            where TElement : class, ISearchSourceElement
        {
            var asUpdateable = element as IAmFoundByUpdatable;
            if (asUpdateable != null)
            {

                var bysAsString = bys.AppendByClass(element.Class).Select(by => by.ToString()).Join(", ");
                asUpdateable.UpdateFoundBy(bysAsString);
            }
        }

        private class ElementAndDepth
        {
            public object NativeElement { get; private set; }
            public ISearchSourceElement Parent { get; private set; }
            public int Depth { get; private set; }

            public ElementAndDepth(object nativeElement, ISearchSourceElement parent, int depth)
            {
                NativeElement = nativeElement;
                Parent = parent;
                Depth = depth;
            }
        }
    }
}