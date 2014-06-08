using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class LabelTest : TestBase
    {
        [Test]
        public void LabelContent()
        {
            var stuffControl = MainWindow.StuffControl;
            WpfLabel inputLabel = stuffControl.InputLabel;
            inputLabel.AssertThat(x => x.Content(), Is.EqualTo("_Input:"));
        }
    }
}