using System;
using System.Linq.Expressions;

namespace tungsten.core
{
    public class By
    {
        private readonly Expression<Func<WpfElement, bool>> _predicateExp;
        private readonly Func<WpfElement, bool> _predicate;

        private By(Expression<Func<WpfElement, bool>> predicateExp)
        {
            _predicateExp = predicateExp;
            _predicate = predicateExp.Compile();
        }

        public bool Matches(WpfElement element)
        {
            return _predicate(element);
        }

        public static By Name(string name)
        {
            return new By(element => element.Name == name);
        }

        public override string ToString()
        {
            return _predicateExp.ToString();
        }
    }
}