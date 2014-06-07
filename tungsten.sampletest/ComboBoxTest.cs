using System.Linq;
using NUnit.Framework;
using tungsten.core;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.nunit;

namespace tungsten.sampletest
{
    [TestFixture]
    public class ComboBoxTest : TestBase
    {
        [Test]
        public void CheckBoxContents()
        {
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var comboBox = window.FindFirstChild<WpfComboBox>(By.Name("CmbShowError"));
            comboBox.AssertThat(x => x.Items.Select(i => i.Content), Is.EqualTo(new[]
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
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var comboBox = window.FindFirstChild<WpfComboBox>(By.Name("CmbShowError"));
            comboBox.AssertThat(x => x.SelectedItem.Content, Is.EqualTo("No error"));
        }

        [Test]
        public void CheckBoxChangeSelectedItemUsingString()
        {
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var comboBox = window.FindFirstChild<WpfComboBox>(By.Name("CmbShowError"));
            comboBox.ChangeSelectedItemTo("Has error");
            comboBox.AssertThat(x => x.SelectedItem.Content, Is.EqualTo("Has error"));
        }

        [Test]
        public void CheckBoxChangeSelectedItemToNonExisting()
        {
            // Unfortunately a very slow test. Failures are slow.
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var comboBox = window.FindFirstChild<WpfComboBox>(By.Name("CmbShowError"));
            comboBox.AssertThrows(typeof(ElementNotFoundException), x => x.ChangeSelectedItemTo("Clearly does not exist"));
        }

        [Test]
        public void CheckBoxChangeSelectedItemRequiresScrolling()
        {
            var window = Desktop.FindFirstChild<WpfWindow>(By.Name("WndMain"));
            var comboBox = window.FindFirstChild<WpfComboBox>(By.Name("CmbShowError"));

            var lastItem = comboBox.Items.Last();
            lastItem.AssertThat(x => x.Content, Is.EqualTo("Item 6"));
            comboBox.ChangeSelectedItemTo(lastItem);
            comboBox.AssertThat(x => x.SelectedItem.Content, Is.EqualTo("Item 6"));

            var firstItem = comboBox.Items.First();
            firstItem.AssertThat(x => x.Content, Is.EqualTo("No error"));
            comboBox.ChangeSelectedItemTo(firstItem);
            comboBox.AssertThat(x => x.SelectedItem.Content, Is.EqualTo("No error"));
        }
    }
}