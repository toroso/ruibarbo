using NUnit.Framework;
using ruibarbo.core.Wpf.Base;
using ruibarbo.nunit;
using ruibarbo.sampletest.AutomationLayer;

namespace ruibarbo.sampletest.Features
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

        [Test]
        public void RadioButtonsKeyboardFocus()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            var disableSubmit = stuffControl.DisabledSubmitRadioButton;
            var enableSubmit = stuffControl.EnabledSubmitRadioButton;
            disableSubmit.AssertThat(x => x.IsKeyboardFocused(), Is.False);
            enableSubmit.AssertThat(x => x.IsKeyboardFocused(), Is.False);
            disableSubmit.Click();
            disableSubmit.AssertThat(x => x.IsKeyboardFocused(), Is.True);
            enableSubmit.AssertThat(x => x.IsKeyboardFocused(), Is.False);
            enableSubmit.Click();
            disableSubmit.AssertThat(x => x.IsKeyboardFocused(), Is.False);
            enableSubmit.AssertThat(x => x.IsKeyboardFocused(), Is.True);
        }

        [Test]
        public void ChangeTab()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            var inputTextBox = stuffControl.InputTextBox;
            inputTextBox.Click();
            inputTextBox.AssertThat(x => x.IsKeyboardFocused(), Is.True);

            var tab2 = MainWindow.MainTabControl.Tab2;
            tab2.Click();
            inputTextBox.AssertThat(x => x.IsKeyboardFocused(), Is.False);

            tab1.Click();
            tab1.ShowStuffCheckBox.AssertThat(x => x.IsKeyboardFocused(), Is.True); // Because it has TabIndex=1 within Tab1
        }
    }
}