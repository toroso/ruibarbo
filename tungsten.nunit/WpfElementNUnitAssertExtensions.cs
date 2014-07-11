using System;
using System.Linq.Expressions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using tungsten.core;
using tungsten.core.Hardware;
using tungsten.core.Search;

namespace tungsten.nunit
{
    public static class WpfElementNUnitAssertExtensions
    {
        public static void AssertThat<TWpfElement>(
                this TWpfElement me,
                Expression<Func<TWpfElement, object>> actualExp,
                IResolveConstraint resolveConstraint)
            where TWpfElement : ISearchSourceElement
        {
            Constraint constraint = resolveConstraint.Resolve();
            var extractFunc = actualExp.Compile();

            var found = Wait.Until(() => constraint.Matches(extractFunc(me)));
            if (!found)
            {
                MessageWriter writer = new TextMessageWriter(null, null);
                writer.WriteMessageLine(string.Empty);
                constraint.WriteMessageTo(writer);
                writer.WriteMessageLine("Expr:     {0}", actualExp);
                writer.WriteMessageLine("Control:  {0}", me.ControlIdentifier());
                writer.WriteMessageLine("Path:\n{0}", me.ControlIdentifierPath());
                var screenCapture = Screen.CaptureToFile("NUnitAssertion");
                if (screenCapture != null)
                {
                    writer.WriteMessageLine("Screen:   {0}", screenCapture.AbsoluteUri);
                }
                throw new AssertionException(writer.ToString());
            }
        }

        public static void AssertThrows<TWpfElement>(this TWpfElement me, Type expectedExceptionType, Action<TWpfElement> action)
            where TWpfElement : ISearchSourceElement
        {
            me.AssertThrows(expectedExceptionType, null, action);
        }

        public static void AssertThrows<TWpfElement>(this TWpfElement me, Type expectedExceptionType, string expectedMessage, Action<TWpfElement> action)
            where TWpfElement : ISearchSourceElement
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