using System;
using System.Collections.Generic;

namespace tungsten.core.Utils
{
    internal static class TypeExtensions
    {
        public static IEnumerable<Type> AllTypesInHierarchy(this Type me)
        {
            Type current = me;
            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
        }
    }
}