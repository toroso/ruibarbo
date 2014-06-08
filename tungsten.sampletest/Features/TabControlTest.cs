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
            var mainTabControl = MainWindow.MainTabControl;
            mainTabControl.AssertThat(x => x.TabItems().Count(), Is.EqualTo(3));
        }

        [Test]
        public void FirstTabItemIsSelected()
        {
            var mainTabControl = MainWindow.MainTabControl;
            var tab1 = mainTabControl.TabItems().First();
            tab1.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void TabControlHasFirstTabItemAsSelected()
        {
            var mainTabControl = MainWindow.MainTabControl;
            mainTabControl.AssertThat(x => x.SelectedItem().Header(), Is.EqualTo("Tab 1"));
        }
    }
}