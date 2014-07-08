using System.Linq;
using NUnit.Framework;
using tungsten.core.Wpf;
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
            var muppets = tab5.MuppetsExpander.MuppetsListBox;
            muppets.AssertThat(x => x.AllItems<MuppetListBoxItem>().Count(), Is.EqualTo(18));
        }

        [Test]
        public void IsCorrectMuppet()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsExpander.MuppetsListBox;
            var muppetItem = muppets.AllItems<MuppetListBoxItem>().ToArray()[14];
            muppetItem.MuppetTextBlock.AssertThat(x => x.Text(), Is.EqualTo("Statler"));
        }

        [Test]
        public void NoSelectedItem()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsExpander.MuppetsListBox;
            muppets.AssertThat(x => x.SelectedItem<MuppetListBoxItem>(), Is.Null);
        }

        [Test]
        public void SelectedItemThroughItem()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsExpander.MuppetsListBox;
            var swedishChef = muppets.FindFirstItem<MuppetListBoxItem>(by => by.Muppet("Swedish Chef"));
            swedishChef.AssertThat(x => x.IsSelected(), Is.False);
            swedishChef.Click();
            swedishChef.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void SelectedItemThroughListBox()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsExpander.MuppetsListBox;
            muppets.AssertThat(x => x.SelectedItem<MuppetListBoxItem>(), Is.Null);
            var swedishChef = muppets.FindFirstItem<MuppetListBoxItem>(by => by.Muppet("Swedish Chef"));
            swedishChef.Click();
            muppets.AssertThat(x => x.SelectedItem<MuppetListBoxItem>(), Is.Not.Null);
            var selectedItem = muppets.SelectedItem<MuppetListBoxItem>();
            selectedItem.MuppetTextBlock.AssertThat(x => x.Text(), Is.EqualTo("Swedish Chef"));
            selectedItem.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void ChangeSelectedItemUsingString()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsExpander.MuppetsListBox;
            muppets.ClickFirst<MuppetListBoxItem>(by => by.Muppet("Swedish Chef"));
            var swedishChef = muppets.FindFirstItem<MuppetListBoxItem>(by => by.Muppet("Swedish Chef"));
            swedishChef.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void ChangeSelectedItemRequiresScrolling()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsExpander.MuppetsListBox;

            var lastItem = muppets.AllItems<MuppetListBoxItem>().Last();
            lastItem.AssertThat(x => x.MuppetTextBlock.Text(), Is.EqualTo("Scooter"));
            lastItem.Click();
            lastItem.AssertThat(x => x.IsSelected(), Is.True);

            var firstItem = muppets.AllItems<MuppetListBoxItem>().First();
            firstItem.AssertThat(x => x.MuppetTextBlock.Text(), Is.EqualTo("Animal"));
            firstItem.Click();
            firstItem.AssertThat(x => x.IsSelected(), Is.True);
            lastItem.AssertThat(x => x.IsSelected(), Is.False);
        }
    }
}