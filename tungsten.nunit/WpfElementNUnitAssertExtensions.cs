using System;
using System.Linq.Expressions;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using tungsten.core.Elements;
using tungsten.core.Utils;

namespace tungsten.nunit
{
    public static class WpfElementNUnitAssertExtensions
    {
        public static void AssertThat<TWpfElement>(
                this TWpfElement me,
                Expression<Func<TWpfElement, object>> actualExp,
                IResolveConstraint resolveConstraint)
            where TWpfElement : UntypedWpfElement
        {
            // TODO: Take default retryTime from config. Share with Core or its own?
            me.AssertThat(actualExp, resolveConstraint, TimeSpan.FromSeconds(5));
        }

        public static void AssertThat<TWpfElement>(
                this TWpfElement me,
                Expression<Func<TWpfElement, object>> actualExp,
                IResolveConstraint resolveConstraint,
                TimeSpan maxRetryTime)
            where TWpfElement : UntypedWpfElement
        {
            Constraint constraint = resolveConstraint.Resolve();
            var extractFunc = actualExp.Compile();

            var found = Wait.Until(() => constraint.Matches(extractFunc(me)), maxRetryTime);
            if (!found)
            {
                MessageWriter writer = new TextMessageWriter(null, null);
                writer.WriteMessageLine(string.Empty);
                constraint.WriteMessageTo(writer);
                writer.WriteMessageLine("Expr:     {0}", actualExp);
                writer.WriteMessageLine("Control:  {0} {1}", me.GetType().Name, me.ElementSearchPath());
                throw new AssertionException(writer.ToString());
            }
        }
    }
}