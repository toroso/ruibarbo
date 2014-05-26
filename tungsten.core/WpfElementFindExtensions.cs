using System.Collections.Generic;
using System.Linq;

namespace tungsten.core
{
    public static class WpfElementFindExtensions
    {
        public static IWpfElement FindFirstElement(this IWpfElement parent, params By[] bys)
        {
            // TODO: Throw if null
            // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
            return TryFindFirstElement(parent, bys);
        }

        private static IWpfElement TryFindFirstElement(IWpfElement parent, By[] bys)
        {
            // TODO: Max depth
            // TODO: Make a few attempts
            // TODO: Enqueue children, not parent -- return WpfElement
            var breadthFirstQueue = new Queue<IWpfElement>();
            breadthFirstQueue.Enqueue(parent);

            while (breadthFirstQueue.Count > 0)
            {
                IWpfElement current = breadthFirstQueue.Dequeue();
                if (bys.All(by => by.Matches(current)))
                {
                    return current;
                }

                var children = current.Children;
                foreach (WpfElement child in children)
                {
                    breadthFirstQueue.Enqueue(child);
                }
            }
            return null;
        }
    }
}