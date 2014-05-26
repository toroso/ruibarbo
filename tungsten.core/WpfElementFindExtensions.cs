using System.Collections.Generic;
using System.Linq;

namespace tungsten.core
{
    public static class WpfElementFindExtensions
    {
        public static WpfElement FindFirstElement(this SearchSourceElement parent, params By[] bys)
        {
            // TODO: Throw if null
            // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
            return TryFindFirstElement(parent, bys);
        }

        private static WpfElement TryFindFirstElement(SearchSourceElement parent, By[] bys)
        {
            // TODO: Max depth
            // TODO: Make a few attempts
            var breadthFirstQueue = new Queue<WpfElement>();
            breadthFirstQueue.EnqueueAll(parent.Children);

            while (breadthFirstQueue.Count > 0)
            {
                WpfElement current = breadthFirstQueue.Dequeue();
                if (bys.All(by => by.Matches(current)))
                {
                    return current;
                }

                breadthFirstQueue.EnqueueAll(current.Children);
            }

            return null;
        }
    }
}