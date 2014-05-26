using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace tungsten.core
{
    public class ExpressionLiteralizer : ExpressionVisitor
    {
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType.IsDefined(typeof(CompilerGeneratedAttribute), false) &&
                node.Expression.NodeType == ExpressionType.Constant)
            {
                object target = ((ConstantExpression)node.Expression).Value;
                if (target != null)
                {
                    switch (node.Member.MemberType)
                    {
                        case MemberTypes.Property:
                            return PropertyExpression(node, target);
                        case MemberTypes.Field:
                            return FieldExpression(node, target);
                    }
                }
            }

            return base.VisitMember(node);
        }

        private static Expression PropertyExpression(MemberExpression node, object target)
        {
            var value = ((PropertyInfo) node.Member).GetValue(target, null);
            return Expression.Constant(value, node.Type);
        }

        private static Expression FieldExpression(MemberExpression node, object target)
        {
            var value = ((FieldInfo) node.Member).GetValue(target);
            return Expression.Constant(value, node.Type);
        }
    }
}