using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class CheckBoxTest : TestBase
    {
        [Test]
        public void CheckBoxIsChecked()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var checkBox = tab1.ShowStuffCheckBox;
            checkBox.AssertThat(x => x.IsChecked(), Is.True);
        }

        [Test]
        public void CheckBoxChangeIsChecked()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var checkBox = tab1.ShowStuffCheckBox;
            checkBox.Click();
            checkBox.AssertThat(x => x.IsChecked(), Is.False);
        }
    }
}