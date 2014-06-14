using System;
using System.Collections.Generic;
using System.Linq;

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

        public static bool IsSubclassOfGenericInterface(this Type me, Type generic)
        {
            return me.MatchingGenericInterfaces(generic).Any();
        }

        public static Type GenericTypeArgumentOf(this Type me, Type generic)
        {
            return me.MatchingGenericInterfaces(generic).First().GetGenericArguments()[0];
        }

        private static IEnumerable<Type> MatchingGenericInterfaces(this Type me, Type generic)
        {
            return me.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == generic);
        }
    }
}