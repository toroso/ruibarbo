using System.Linq;
using System.Windows.Controls;
using NUnit.Framework;
using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Search;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class ComboBoxTest : TestBase
    {
        [Test]
        public void ItemsCount()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            comboBox.AssertThat(x => x.AllItems<WpfComboBoxItem>().Count(), Is.EqualTo(29));
        }

        [Test]
        public void CheckBoxContents()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            comboBox.AssertThat(x => x.AllItems<WpfComboBoxItem>().Select(i => i.TextBlockText()), Is.EqualTo(new[]
                {
                    "No error",
                    "Has error",
                    "Item 3",
                    "Item 4",
                    "Item 5",
                    "Item 6",
                    "Item 7",
                    "Item 8",
                    "Item 9",
                    "Item 10",
                    "Item 11",
                    "Item 12",
                    "Item 13",
                    "Item 14",
                    "Item 15",
                    "Item 16",
                    "Item 17",
                    "Item 18",
                    "Item 19",
                    "Item 20",
                    "Item 21",
                    "Item 22",
                    "Item 23",
                    "Item 24",
                    "Item 25",
                    "Item 26",
                    "Item 27",
                    "Item 28",
                    "Item 29",
                }));
        }

        [Test]
        public void CheckBoxSelectedItem()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            var noErrorItem = comboBox.FindFirstItem<WpfComboBoxItem>(ByWpf.TextBlockText("No error"));
            noErrorItem.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void CheckBoxChangeSelectedItemUsingString()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            var item = comboBox.FindFirstItem<WpfComboBoxItem>(By.Custom<WpfComboBoxItem>(i => i.TextBlockText(), "Has error"));
            item.OpenAndClick();
            item.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void CheckBoxChangeSelectedItemToNonExisting()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            var doesNotExist = new WpfComboBoxItem(comboBox, Invoker.Get(() => new ComboBoxItem()));
            doesNotExist.AssertThrows(typeof (ManglaException), x => x.OpenAndClick()); // Throws because it has no ComboBox ancestor
        }

        [Test]
        public void CheckBoxChangeSelectedItemRequiresScrolling()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;

            var lastItem = comboBox.AllItems<WpfComboBoxItem>().Last();
            lastItem.AssertThat(x => x.TextBlockText(), Is.EqualTo("Item 29"));
            lastItem.OpenAndClick();
            lastItem.AssertThat(x => x.IsSelected(), Is.True);

            var firstItem = comboBox.AllItems<WpfComboBoxItem>().First();
            firstItem.AssertThat(x => x.TextBlockText(), Is.EqualTo("No error"));
            firstItem.OpenAndClick();
            firstItem.AssertThat(x => x.IsSelected(), Is.True);
            lastItem.AssertThat(x => x.IsSelected(), Is.False);
        }
    }
}