using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public class By
    {
        private readonly Expression<Func<ISearchSourceElement, object>> _extractExp;
        private readonly Func<ISearchSourceElement, object> _extractFunc;
        private readonly object _searchFor;

        private By(Expression<Func<ISearchSourceElement, object>> extractExp, object searchFor)
        {
            _extractExp = extractExp;
            _extractFunc = extractExp.Compile();
            _searchFor = searchFor;
        }

        public bool Matches(ISearchSourceElement element)
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
            var literalizer = new ExpressionLiteralizer();
            var sanitized = (Expression<Func<ISearchSourceElement, object>>)literalizer.Visit(_extractExp);
            string asString = sanitized.Body.ToString();
            var extractAsString = asString.StartsWith("(") && asString.EndsWith(")")
                ? asString.Substring(1, asString.Length - 2)
                : asString;
            // TODO: Put quotes around _searchFor if string
            return string.Format("{0} == {1}", extractAsString, _searchFor);
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
    }
}