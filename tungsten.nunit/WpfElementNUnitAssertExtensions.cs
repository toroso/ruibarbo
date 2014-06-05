﻿using System;
using System.Linq.Expressions;
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

        public static void AssertThrows<TWpfElement>(this TWpfElement me, Type expectedExceptionType, Action<TWpfElement> action)
            where TWpfElement : UntypedWpfElement
        {
            me.AssertThrows(expectedExceptionType, null, action);
        }

        public static void AssertThrows<TWpfElement>(this TWpfElement me, Type expectedExceptionType, string expectedMessage, Action<TWpfElement> action)
            where TWpfElement : UntypedWpfElement
        {
            try
            {
                action(me);
            }
            catch (Exception actualException)
            {
                if (actualException.GetType() != expectedExceptionType)
                {
                    FailDueToWrongException(expectedExceptionType, actualException);
                }
                if (expectedMessage != null && actualException.Message != expectedMessage)
                {
                    FailDueToWrongMessage(expectedMessage, actualException.Message);
                }
                return; // Success
            }
            FailDueToNoException(expectedExceptionType);
        }

        private static void FailDueToNoException(Type expectedExceptionType)
        {
            FailDueToWrongException(expectedExceptionType, null);
        }

        private static void FailDueToWrongException(Type expectedExceptionType, Exception actualException)
        {
            var actual = actualException != null ? actualException.ToString() : "<null>";
            Assert.Fail(
@"Expected exception was not fired.
  Expected: {0}
  Actual:   {1}", expectedExceptionType, actual);
        }

        private static void FailDueToWrongMessage(string expected, string actual)
        {
            Assert.Fail(
@"Exception contained wrong message.
  Expected: {0}
  Actual:   {1}", expected, actual);
        }
    }
}