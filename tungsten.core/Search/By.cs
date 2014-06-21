using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using tungsten.core.Elements;
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

        internal static By Class(Type type)
        {
            return new By(element => element.Class == type);
        }

        public static By Content(string content)
        {
            return new By(element => CompareContent(content, element));
        }

        private static bool CompareContent(string content, ISearchSourceElement element)
        {
            var contentControl = element as IContentControl;
            return contentControl != null && contentControl.Content().Equals(content);
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
    }

    public static class ByExtensions
    {
        public static IEnumerable<By> AppendByClass<T>(this IEnumerable<By> bys)
        {
            return bys.AppendByClass(typeof(T));
        }

        public static IEnumerable<By> AppendByClass(this IEnumerable<By> bys, Type type)
        {
            return bys.Concat(new[] { By.Class(type) });
        }
    }
}