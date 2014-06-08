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
            var checkBox = MainWindow.ShowStuffCheckBox;
            checkBox.AssertThat(x => x.IsChecked(), Is.True);
            checkBox.Click();
            checkBox.AssertThat(x => x.IsChecked(), Is.False);
        }

        [Test]
        public void CheckBoxChangeIsChecked()
        {
            var checkBox = MainWindow.ShowStuffCheckBox;
            checkBox.Click();
            checkBox.AssertThat(x => x.IsChecked(), Is.False);
        }
    }
}