using NUnit.Framework;
using tungsten.core.Elements;
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
            var stuffControl = MainWindow.StuffControl;
            WpfTextBlock errorTextBlock = stuffControl.ErrorTextBlock;
            errorTextBlock.AssertThat(x => x.IsVisible(), Is.False);
            errorTextBlock.AssertThat(x => x.Text(), Is.EqualTo("Naughty frog!"));
        }

        [Test]
        public void TextBlockVisible()
        {
            var stuffControl = MainWindow.StuffControl;
            stuffControl.ShowErrorComboBox.ChangeSelectedItemTo("Has error");
            WpfTextBlock errorTextBlock = stuffControl.ErrorTextBlock;
            errorTextBlock.AssertThat(x => x.IsVisible(), Is.True);
            errorTextBlock.AssertThat(x => x.Text(), Is.EqualTo("Naughty frog!"));
        }
    }
}