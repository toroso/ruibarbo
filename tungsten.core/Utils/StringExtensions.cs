using System.Collections.Generic;
using System.Linq;

namespace tungsten.core.Utils
{
    internal static class StringExtensions
    {
        public static string JoinExcludeEmpty(this IEnumerable<string> me, string separator)
        {
            return string.Join(separator, me.Where(each => !string.IsNullOrEmpty(each)));
        }

        public static string Join(this IEnumerable<string> me, string separator)
        {
            return string.Join(separator, me);
        }
    }
}