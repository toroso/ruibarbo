using System;
using System.Linq.Expressions;
using tungsten.core.Elements;
using tungsten.core.Utils;

namespace tungsten.core.Search
{
    public class By
    {
        private readonly Expression<Func<UntypedWpfElement, bool>> _predicateExp;
        private readonly Func<UntypedWpfElement, bool> _predicate;

        private By(Expression<Func<UntypedWpfElement, bool>> predicateExp)
        {
            _predicateExp = predicateExp;
            _predicate = predicateExp.Compile();
        }

        public bool Matches(UntypedWpfElement element)
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
            // TODO: Replace WpfComboBoxItem with WpfContentControl
            return new By(element => ((WpfComboBoxItem)element).Content().Equals(content));
        }

        public override string ToString()
        {
            var literalizer = new ExpressionLiteralizer();
            var sanitized = (Expression<Func<UntypedWpfElement, bool>>)literalizer.Visit(_predicateExp);
            string asString = sanitized.Body.ToString();
            return asString.StartsWith("(") && asString.EndsWith(")")
                ? asString.Substring(1, asString.Length - 2)
                : asString;
        }
    }
}