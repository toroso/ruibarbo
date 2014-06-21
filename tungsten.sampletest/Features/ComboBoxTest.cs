﻿using System.Linq;
using System.Windows.Controls;
using NUnit.Framework;
using tungsten.core;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class ComboBoxTest : TestBase
    {
        [Test]
        public void CheckBoxContents()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            comboBox.AssertThat(x => x.AllItems<WpfComboBoxItem>().Select(i => i.Content()), Is.EqualTo(new[]
                {
                    "No error",
                    "Has error",
                    "Item 3",
                    "Item 4",
                    "Item 5",
                    "Item 6",
                }));
        }

        [Test]
        public void CheckBoxSelectedItem()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            var noErrorItem = comboBox.FindFirstItem<WpfComboBoxItem>(By.Content("No error"));
            noErrorItem.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void CheckBoxChangeSelectedItemUsingString()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var comboBox = tab1.StuffControl.ShowErrorComboBox;
            var item = comboBox.FindFirstItem<WpfComboBoxItem>(By.Content("Has error"));
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
            lastItem.AssertThat(x => x.Content(), Is.EqualTo("Item 6"));
            lastItem.OpenAndClick();
            lastItem.AssertThat(x => x.IsSelected(), Is.True);

            var firstItem = comboBox.AllItems<WpfComboBoxItem>().First();
            firstItem.AssertThat(x => x.Content(), Is.EqualTo("No error"));
            firstItem.OpenAndClick();
            firstItem.AssertThat(x => x.IsSelected(), Is.True);
            lastItem.AssertThat(x => x.IsSelected(), Is.False);
        }
    }
}