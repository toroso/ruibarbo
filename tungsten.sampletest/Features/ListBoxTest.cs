using System.Linq;
using NUnit.Framework;
using tungsten.core.Wpf.Base;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class ListBoxTest : TestBase
    {
        [Test]
        public void ItemsCount()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsListBox;
            muppets.AssertThat(x => x.AllItems<MuppetListBoxItem>().Count(), Is.EqualTo(18));
        }

        [Test]
        public void IsCorrectMuppet()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsListBox;
            var muppetItem = muppets.AllItems<MuppetListBoxItem>().ToArray()[14];
            muppetItem.MuppetTextBlock.AssertThat(x => x.Text(), Is.EqualTo("Statler"));
        }

        [Test]
        public void NoSelectedItem()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsListBox;
            muppets.AssertThat(x => x.SelectedItem<MuppetListBoxItem>(), Is.Null);
        }

        [Test]
        public void SelectedItemThroughItem()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsListBox;
            var yolanda = muppets.FindFirstItem<MuppetListBoxItem>(by => by.Muppet("Yolanda the Rat"));
            yolanda.AssertThat(x => x.IsSelected(), Is.False);
            yolanda.Click();
            yolanda.AssertThat(x => x.IsSelected(), Is.True);
        }
    }
}