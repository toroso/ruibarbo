using NUnit.Framework;
using tungsten.core.Wpf;
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
            tab1.StuffControl.SubmitButton.Click();
            MessageBox.OkButton.Click();
        }

        [Test]
        public void CloseMessageBoxWithCancelButton()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            tab1.StuffControl.SubmitButton.Click();
            MessageBox.CancelButton.Click();
        }
    }
}