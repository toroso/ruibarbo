using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class UserControlTest : TestBase
    {
        [Test]
        public void StuffControlIsVisible()
        {
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var stuffControl = window.FindFirstChild<StuffControl>(By.Name("CtrlStuff"));
            stuffControl.AssertThat(x => x.IsVisible(), Is.True);
        }
    }
}