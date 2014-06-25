using NUnit.Framework;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class IsVisibleTest : TestBase
    {
        [Test]
        public void StuffControlIsVisible()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            stuffControl.AssertThat(x => x.IsVisible, Is.True);
        }

        [Test]
        public void StuffControlIsMadeNotVisible()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var checkBox = tab1.ShowStuffCheckBox;
            checkBox.Click();
            var stuffControl = tab1.StuffControl;
            stuffControl.AssertThat(x => x.IsVisible, Is.False);
        }
    }
}