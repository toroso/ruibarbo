using NUnit.Framework;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class MessageBoxTest : TestBase
    {
        [Test]
        public void CloseMessageBoxWithOkButton()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var submitButton = tab1.StuffControl.SubmitButton;
            submitButton.Click();
            MessageBox.OkButton.Click();
        }
    }
}