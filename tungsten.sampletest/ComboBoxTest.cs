using System.Linq;
using NUnit.Framework;
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
            var window = Desktop.FindFirstElement<WpfWindow>(By.Name("WndMain"));
            var comboBox = window.FindFirstElement<WpfComboBox>(By.Name("CmbShowError"));
            comboBox.AssertThat(x => x.Items.Select(i => i.Content), Is.EqualTo(new[] { "No error", "Has error" }));
        }

        [Test]
        public void CheckBoxSelectedItem()
        {
            var window = Desktop.FindFirstElement<WpfWindow>(By.Name("WndMain"));
            var comboBox = window.FindFirstElement<WpfComboBox>(By.Name("CmbShowError"));
            comboBox.AssertThat(x => x.SelectedItem.Content, Is.EqualTo("No error"));
        }
    }
}