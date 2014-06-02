using System;
using System.Linq.Expressions;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.sampletest
{
    public class CheckBoxTest : TestBase
    {
        [Test]
        public void Hupp()
        {
            var window = Desktop.FindFirstElement<WpfWindow>(By.Name("WndMain"));
            var checkBox = window.FindFirstElement<WpfCheckBox>(By.Name("ShowStuff"));
            checkBox.AssertThat(x => x.IsChecked, Is.True);
        }
    }

    // TODO: Move to other assembly: tungsten.nunit
    public static class WpfElementNUnitAssertExtensions
    {
        public static void AssertThat<TWpfElement>(
                this TWpfElement me,
                Expression<Func<TWpfElement, object>> actualExp,
                IResolveConstraint resolveConstraint)
            where TWpfElement : WpfElement
        {
            // TODO: Take default retryTime from config
            me.AssertThat(actualExp, resolveConstraint, TimeSpan.FromSeconds(5));
        }

        public static void AssertThat<TWpfElement>(
                this TWpfElement me,
                Expression<Func<TWpfElement, object>> actualExp,
                IResolveConstraint resolveConstraint,
                TimeSpan maxRetryTime)
            where TWpfElement : WpfElement
        {
            Constraint constraint = resolveConstraint.Resolve();
            var extractFunc = actualExp.Compile();

            var found = WaitUntil(() => constraint.Matches(extractFunc(me)), maxRetryTime);
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

        private static bool WaitUntil(Func<bool> found, TimeSpan maxRetryTime)
        {
            var sleepTime = TimeSpan.FromMilliseconds(10);
            DateTime retryUntil = DateTime.Now + maxRetryTime;
            while (DateTime.Now < retryUntil)
            {
                if (found())
                {
                    return true;
                }

                Thread.Sleep(sleepTime);
            }

            return false;
        }
    }
}