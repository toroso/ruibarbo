using System.Linq;
using NUnit.Framework;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class ItemsControlTest : TestBase
    {
        [Test]
        public void ItemsCount()
        {
            var tab4 = MainWindow.MainTabControl.Tab4;
            tab4.Click();
            var muppets = tab4.MuppetsItemsControl;
            muppets.AssertThat(x => x.AllItems<MuppetItem>().Count(), Is.EqualTo(18));
        }
    }
}