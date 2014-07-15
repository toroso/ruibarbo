using System;
using System.Linq.Expressions;

namespace ruibarbo.core.Utils
{
    internal static class ExpressionMerger
    {
        public static Expression<Func<TParameter, TResult>> MergeWith<TParameter, TIntermediate, TResult>(
            this Expression<Func<TParameter, TIntermediate>> extractExp,
            Expression<Func<TIntermediate, TResult>> equalsExp)
        {
            var resultBody = equalsExp.Body.Replace(equalsExp.Parameters[0], extractExp.Body);
            return Expression.Lambda<Func<TParameter, TResult>>(resultBody, extractExp.Parameters[0]);
        }

        public static Expression Replace(this Expression expression, Expression searchEx, Expression replaceEx)
        {
            return new ReplaceVisitor(searchEx, replaceEx).Visit(expression);
        }

        internal class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression _from;
            private readonly Expression _to;

            public ReplaceVisitor(Expression from, Expression to)
            {
                _from = from;
                _to = to;
            }

            public override Expression Visit(Expression node)
            {
                return node == _from
                    ? _to
                    : base.Visit(node);
            }
        }
    }
}