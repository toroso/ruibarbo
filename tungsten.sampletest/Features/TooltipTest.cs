using NUnit.Framework;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Search;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class TooltipTest : TestBase
    {
        [Test]
        public void CheckNonVisibleTooltip()
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
            text.AssertThat(x => x.Text(), Is.EqualTo("It's sad that something bad went very wrong."));
        }
    }
}