using System.Linq;
using NUnit.Framework;
using tungsten.core;
using tungsten.core.Elements;
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
            var comboBox = MainWindow.StuffControl.ShowErrorComboBox;
            comboBox.AssertThat(x => x.Items().Select(i => i.Content()), Is.EqualTo(new[]
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
            var comboBox = MainWindow.StuffControl.ShowErrorComboBox;
            comboBox.AssertThat(x => x.SelectedItem().Content(), Is.EqualTo("No error"));
        }

        [Test]
        public void CheckBoxChangeSelectedItemUsingString()
        {
            var comboBox = MainWindow.StuffControl.ShowErrorComboBox;
            comboBox.ChangeSelectedItemTo("Has error");
            comboBox.AssertThat(x => x.SelectedItem().Content(), Is.EqualTo("Has error"));
        }

        [Test]
        public void CheckBoxChangeSelectedItemToNonExisting()
        {
            // Unfortunately a very slow test. Failures are slow.
            var comboBox = MainWindow.StuffControl.ShowErrorComboBox;
            comboBox.AssertThrows(typeof(ManglaException), x => x.ChangeSelectedItemTo("Clearly does not exist"));
        }

        [Test]
        public void CheckBoxChangeSelectedItemRequiresScrolling()
        {
            var comboBox = MainWindow.StuffControl.ShowErrorComboBox;

            var lastItem = comboBox.Items().Last();
            lastItem.AssertThat(x => x.Content(), Is.EqualTo("Item 6"));
            comboBox.ChangeSelectedItemTo(lastItem);
            comboBox.AssertThat(x => x.SelectedItem().Content(), Is.EqualTo("Item 6"));

            var firstItem = comboBox.Items().First();
            firstItem.AssertThat(x => x.Content(), Is.EqualTo("No error"));
            comboBox.ChangeSelectedItemTo(firstItem);
            comboBox.AssertThat(x => x.SelectedItem().Content(), Is.EqualTo("No error"));
        }
    }
}