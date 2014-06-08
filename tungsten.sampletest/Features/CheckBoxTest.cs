using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.core.Search;
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
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var checkBox = window.FindFirstChild<WpfCheckBox>(By.Name("ShowStuff"));
            checkBox.AssertThat(x => x.IsChecked(), Is.True);

            checkBox.Click();
            checkBox.AssertThat(x => x.IsChecked(), Is.False);
        }

        [Test]
        public void CheckBoxChangeIsChecked()
        {
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var checkBox = window.FindFirstChild<WpfCheckBox>(By.Name("ShowStuff"));

            checkBox.Click();
            checkBox.AssertThat(x => x.IsChecked(), Is.False);
        }
    }
}