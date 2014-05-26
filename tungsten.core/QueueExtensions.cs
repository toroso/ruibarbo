using System.Collections.Generic;

namespace tungsten.core
{
    public static class QueueExtensions
    {
        public static void EnqueueAll<T>(this Queue<T> me, IEnumerable<T> all)
        {
            foreach (var each in all)
            {
                me.Enqueue(each);
            }
        }
    }
}