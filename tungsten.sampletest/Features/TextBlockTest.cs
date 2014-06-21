using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class TextBlockTest : TestBase
    {
        [Test]
        public void TextBlockNoVisible()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            WpfTextBlock errorTextBlock = stuffControl.ErrorTextBlock;
            errorTextBlock.AssertThat(x => x.IsVisible, Is.False);
            errorTextBlock.AssertThat(x => x.Text(), Is.EqualTo("Naughty frog!"));
        }

        [Test]
        public void TextBlockVisible()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            var comboBox = stuffControl.ShowErrorComboBox;
            var item = comboBox.FindFirstItem<WpfComboBoxItem>(By.Content("Has error"));
            item.OpenAndClick();
            WpfTextBlock errorTextBlock = stuffControl.ErrorTextBlock;
            errorTextBlock.AssertThat(x => x.IsVisible, Is.True);
            errorTextBlock.AssertThat(x => x.Text(), Is.EqualTo("Naughty frog!"));
        }
    }
}