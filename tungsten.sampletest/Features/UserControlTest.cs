using NUnit.Framework;
using tungsten.core.Elements;
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
            var stuffControl = MainWindow.StuffControl;
            stuffControl.AssertThat(x => x.IsVisible(), Is.True);
        }
    }
}