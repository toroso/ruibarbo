using System;
using System.Linq.Expressions;

namespace tungsten.core
{
    public class By
    {
        private readonly Expression<Func<IWpfElement, bool>> _predicateExp;
        private readonly Func<IWpfElement, bool> _predicate;

        private By(Expression<Func<IWpfElement, bool>> predicateExp)
        {
            _predicateExp = predicateExp;
            _predicate = predicateExp.Compile();
        }

        public bool Matches(IWpfElement element)
        {
            return _predicate(element);
        }

        public static By Name(string name)
        {
            return new By(element => element.Name == name);
        }
    }
}