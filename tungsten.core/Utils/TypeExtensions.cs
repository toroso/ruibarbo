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

        public static bool IsSubclassOfGeneric(this Type me, Type generic)
        {
            var toCheck = me;
            while (toCheck != null && toCheck != typeof(object))
            {
                var current = toCheck.IsGenericType
                    ? toCheck.GetGenericTypeDefinition()
                    : toCheck;

                if (generic == current)
                {
                    return true;
                }

                toCheck = toCheck.BaseType;
            }

            return false;
        }

        public static Type GenericTypeArgumentOf(this Type me, Type generic)
        {
            var toCheck = me;
            while (toCheck != null && toCheck != typeof (object))
            {
                var current = toCheck.IsGenericType
                    ? toCheck.GetGenericTypeDefinition()
                    : toCheck;

                if (generic == current)
                {
                    return toCheck.GetGenericArguments()[0];
                }

                toCheck = toCheck.BaseType;
            }

            return null;
        }
    }
}