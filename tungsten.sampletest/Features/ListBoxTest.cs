using System.Linq;
using NUnit.Framework;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class ListBoxTest : TestBase
    {
        [Test]
        public void ItemsCount()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var muppets = tab5.MuppetsListBox;
            // TODO: The following line throws because Visuals are not created for non-visible items
            muppets.AssertThat(x => x.AllItems<MuppetListBoxItem>().Count(), Is.EqualTo(12));
        }
    }
}