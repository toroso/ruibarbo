using NUnit.Framework;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;
using tungsten.nunit;
using tungsten.sampletest.AutomationLayer;

namespace tungsten.sampletest.Features
{
    [TestFixture]
    public class ExpanderTest : TestBase
    {
        [Test]
        public void ExpanderIsExpanded()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.MuppetsExpander;
            expander.AssertThat(x => x.IsExpanded, Is.True);
        }

        [Test]
        public void ExpanderContantsIsVisibleWhenExpanded()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.MuppetsExpander;
            expander.MuppetsListBox.AssertThat(x => x.IsVisible, Is.True);
        }

        [Test]
        public void ExpanderIsClosed()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.MuppetsExpander;
            expander.ExpandButton<WpfFrameworkElement>().Click();
            expander.AssertThat(x => x.IsExpanded, Is.False);
        }

        [Test]
        public void ExpanderContantsIsNotVisibleWhenClosed()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.MuppetsExpander;
            expander.ExpandButton<WpfFrameworkElement>().Click();
            expander.MuppetsListBox.AssertThat(x => x.IsVisible, Is.False);
        }

        [Test]
        public void ExpanderHeaderTitle()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.MuppetsExpander;
            var title = expander.FindFirstChild<WpfTextBlock>();
            title.AssertThat(x => x.Text(), Is.EqualTo("Muppets"));
        }
    }
}