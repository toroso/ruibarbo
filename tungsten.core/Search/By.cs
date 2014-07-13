using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using tungsten.core.ElementFactory;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public sealed class By
    {
        public static By[] Empty = { };

        private readonly Expression<Func<ISearchSourceElement, object>> _extractExp;
        private readonly Func<ISearchSourceElement, object> _extractFunc;
        private readonly object _searchFor;

        private By(Expression<Func<ISearchSourceElement, object>> extractExp, object searchFor)
        {
            _extractExp = extractExp;
            _extractFunc = extractExp.Compile();
            _searchFor = searchFor;
        }

        internal bool Matches(ISearchSourceElement element)
        {
            object found = _extractFunc(element);
            return found.Equals(_searchFor);
        }

        public static By Name(string name)
        {
            return new By(element => element.Name, name);
        }

        public static By Class(string type)
        {
            return new By(element => element.Class, type);
        }

        public override string ToString()
        {
            var extractAsString = ExtractAsString;
            return string.Format("{0} == {1}", extractAsString, Quote(_searchFor));
        }

        internal string ExtractedToString(ISearchSourceElement element)
        {
            var extractAsString = ExtractAsString;
            object found = _extractFunc(element);
            return string.Format("{0}: {1}", extractAsString, Quote(found));
        }

        private static string Quote(object asObject)
        {
            var asString = asObject as string;
            return asString != null
                ? string.Format("'{0}'", asString)
                : asObject.ToString();
        }

        internal string ExtractAsString
        {
            get
            {
                string bodyAsString = _extractExp.Body.ToString();
                const string convertX = "Convert(x)";
                return bodyAsString.StartsWith(convertX)
                    ? string.Format("element{0}", bodyAsString.Substring(convertX.Length))
                    : bodyAsString;
            }
        }

        internal static By Custom<TElement>(Expression<Func<TElement, object>> extractExp, object searchFor)
            where TElement : class, ISearchSourceElement
        {
            Expression<Func<ISearchSourceElement, TElement>> castExp = x => (TElement)x;
            return new By(castExp.MergeWith(extractExp), searchFor);
        }
    }

    public static class ByExtensions
    {
        public static IEnumerable<By> AppendByClass<T>(this IEnumerable<By> bys)
        {
            return bys.AppendByClass(typeof(T).FullName);
        }

        public static IEnumerable<By> AppendByClass(this IEnumerable<By> bys, string type)
        {
            return bys.Concat(new[] { By.Class(type) });
        }

        public static IEnumerable<By> RemoveByName(this IEnumerable<By> bys)
        {
            return bys.Where(by => by.ExtractAsString != "element.Name");
        }
    }
}