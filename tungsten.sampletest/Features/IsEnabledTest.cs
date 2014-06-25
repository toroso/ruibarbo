using NUnit.Framework;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class IsEnabledTest : TestBase
    {
        [Test]
        public void SubmitButtonIsEnabled()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            stuffControl.SubmitButton.AssertThat(x => x.IsEnabled, Is.True);
        }

        [Test]
        public void SubmitButtonIsMadeNotEnabled()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            stuffControl.DisabledSubmitRadioButton.Click();
            stuffControl.SubmitButton.AssertThat(x => x.IsEnabled, Is.False);
        }
    }
}