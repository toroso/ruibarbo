using NUnit.Framework;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.nunit;
using ruibarbo.sampletest.AutomationLayer;

namespace ruibarbo.sampletest.Features
{
    [TestFixture]
    public class LabelTest : TestBase
    {
        [Test]
        public void LabelContent()
        {
            var tab1 = MainWindow.MainTabControl.Tab1;
            tab1.Click();
            var stuffControl = tab1.StuffControl;
            WpfLabel inputLabel = stuffControl.InputLabel;
            inputLabel.AssertThat(x => x.Content(), Is.EqualTo("_Input:"));
        }
    }
}