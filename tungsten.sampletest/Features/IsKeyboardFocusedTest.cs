using NUnit.Framework;
using tungsten.core.Wpf.Base;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class IsKeyboardFocusedTest : TestBase
    {
        [Test]
        public void TextBoxKeyboardFocus()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            var inputTextBox = stuffControl.InputTextBox;
            inputTextBox.AssertThat(x => x.IsKeyboardFocused(), Is.False);
            inputTextBox.Click();
            inputTextBox.AssertThat(x => x.IsKeyboardFocused(), Is.True);
        }
    }
}