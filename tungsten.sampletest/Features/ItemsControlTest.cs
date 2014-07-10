using System.Linq;
using NUnit.Framework;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class ItemsControlTest : TestBase
    {
        [Test]
        public void ItemsCount()
        {
            var tab4 = MainWindow.MainTabControl.Tab4;
            tab4.Click();
            var muppets = tab4.Muppets4Expander.MuppetsItemsControl;
            muppets.AssertThat(x => x.AllItems<MuppetItemsControlItem>().Count(), Is.EqualTo(18));
        }

        [Test]
        public void IsCorrectMuppet()
        {
            var tab4 = MainWindow.MainTabControl.Tab4;
            tab4.Click();
            var muppets = tab4.Muppets4Expander.MuppetsItemsControl;
            var muppetItem = muppets.AllItems<MuppetItemsControlItem>().ToArray()[16];
            muppetItem.MuppetTextBox.AssertThat(x => x.Text, Is.EqualTo("George the Janitor"));
        }

        [Test]
        public void ChangeMuppet()
        {
            var tab4 = MainWindow.MainTabControl.Tab4;
            tab4.Click();
            var muppets = tab4.Muppets4Expander.MuppetsItemsControl;
            var muppetItem = muppets.AllItems<MuppetItemsControlItem>().ToArray()[16];
            var muppetTextBox = muppetItem.MuppetTextBox;
            muppetTextBox.ClickAndSelectAll();
            muppetTextBox.Type("Crazy Harry");
            muppetTextBox.AssertThat(x => x.Text, Is.EqualTo("Crazy Harry"));
        }
    }
}