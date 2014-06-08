using System.Linq;
using NUnit.Framework;
using tungsten.core.Elements;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class TabControlTest : TestBase
    {
        [Test]
        public void TabItemCount()
        {
            WpfTabControl mainTabControl = MainWindow.MainTabControl;
            mainTabControl.AssertThat(x => x.TabItems().Count(), Is.EqualTo(3));
        }
    }
}