using NUnit.Framework;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Helpers;
using ruibarbo.nunit;
using ruibarbo.sampletest.AutomationLayer;

namespace ruibarbo.sampletest.Features
{
    [TestFixture]
    public class TooltipTest : TestBase
    {
        [Test]
        public void CheckTooltipSpecifiedInXaml()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            var comboBox = stuffControl.ShowErrorComboBox;
            var item = comboBox.FindFirstItem<WpfComboBoxItem>(by => by.FirstTextBlockText("Has error"));
            item.OpenAndClick();
            var errorTextBlock = stuffControl.ErrorTextBlock;
            var tooltip = errorTextBlock.Tooltip<WpfTooltip>();
            tooltip.AssertThat(x => x.IsVisible, Is.False);
            var text = tooltip.FindFirstChild<WpfTextBlock>(By.Name("TxbToolTip"));
            text.AssertThat(x => x.Text, Is.EqualTo("It's sad that something bad went very wrong."));
            errorTextBlock.AssertThat(x => x.TooltipAsString(), Is.Null);
        }

        [Test]
        public void CheckTooltipSpecifiedAsString()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            var submitButton = stuffControl.SubmitButton;
            submitButton.AssertThat(x => x.Tooltip<WpfTooltip>(), Is.Null);
            submitButton.AssertThat(x => x.TooltipAsString(), Is.EqualTo("Opens MessageBox"));
        }
    }
}