using System.Collections.Generic;

namespace ruibarbo.sampleapp
{
    public static class EnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> me, T item)
        {
            int index = 0;
            foreach (var each in me)
            {
                if (each.Equals(item))
                {
                    return index;
                }
                index++;
            }

            return -1;
        }
    }
}