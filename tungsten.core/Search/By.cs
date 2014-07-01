using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public class By
    {
        private readonly Expression<Func<ISearchSourceElement, bool>> _predicateExp;
        private readonly Func<ISearchSourceElement, bool> _predicate;

        private By(Expression<Func<ISearchSourceElement, bool>> predicateExp)
        {
            _predicateExp = predicateExp;
            _predicate = predicateExp.Compile();
        }

        public bool Matches(ISearchSourceElement element)
        {
            return _predicate(element);
        }

        public static By Name(string name)
        {
            return new By(element => element.Name == name);
        }

        public static By Class(string type)
        {
            return new By(element => element.Class == type);
        }

        public static By FirstChild<TElement>(Func<TElement, bool> predicate)
            where TElement : class, ISearchSourceElement
        {
            return new By(element => predicate(element.FindFirstChild<TElement>()));
        }

        public override string ToString()
        {
            var literalizer = new ExpressionLiteralizer();
            var sanitized = (Expression<Func<ISearchSourceElement, bool>>)literalizer.Visit(_predicateExp);
            string asString = sanitized.Body.ToString();
            return asString.StartsWith("(") && asString.EndsWith(")")
                ? asString.Substring(1, asString.Length - 2)
                : asString;
        }

        // TODO: Make internal and let clicks use ByBuilder.
        public static By Custom<TElement>(Func<TElement, object> extractFunc, object searchFor)
            where TElement : class, ISearchSourceElement
        {
            return new By(element => extractFunc((TElement)element).Equals(searchFor));
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