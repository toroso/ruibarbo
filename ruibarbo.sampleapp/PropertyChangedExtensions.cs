using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ruibarbo.sampleapp
{
    public static class PropertyChangedExtensions
    {
        public static void Raise<TViewModel>(this PropertyChangedEventHandler handler, TViewModel vm, Expression<Func<TViewModel, object>> exp)
        {
            if (handler != null)
            {
                handler(vm, new PropertyChangedEventArgs(exp.GetPropertyName()));
            }
        }

        private static string GetPropertyName<TSource, TProperty>(this Expression<Func<TSource, TProperty>> exp)
        {
            var memberExpression = exp.Body as MemberExpression;

            if (memberExpression == null)
            {
                var unaryExpression = (UnaryExpression)exp.Body;
                var unaryMemberExpression = (MemberExpression)unaryExpression.Operand;
                return unaryMemberExpression.Member.Name;
            }

            return memberExpression.Member.Name;
        }
    }
}