using NUnit.Framework;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.nunit;
using ruibarbo.sampletest.AutomationLayer;

namespace ruibarbo.sampletest.Features
{
    [TestFixture]
    public class ExpanderTest : TestBase
    {
        [Test]
        public void ExpanderIsExpanded()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.Muppets5Expander;
            expander.AssertThat(x => x.IsExpanded, Is.True);
        }

        [Test]
        public void ExpanderContantsIsVisibleWhenExpanded()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.Muppets5Expander;
            expander.MuppetsListBox.AssertThat(x => x.IsVisible, Is.True);
        }

        [Test]
        public void ExpanderIsClosed()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.Muppets5Expander;
            expander.ExpandButton<WpfFrameworkElement>().Click();
            expander.AssertThat(x => x.IsExpanded, Is.False);
        }

        [Test]
        public void ExpanderContantsIsNotVisibleWhenClosed()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.Muppets5Expander;
            expander.ExpandButton<WpfFrameworkElement>().Click();
            expander.MuppetsListBox.AssertThat(x => x.IsVisible, Is.False);
        }

        [Test]
        public void ExpanderHeaderTitle()
        {
            var tab5 = MainWindow.MainTabControl.Tab5;
            tab5.Click();
            var expander = tab5.Muppets5Expander;
            var title = expander.FindFirstChild<WpfTextBlock>();
            title.AssertThat(x => x.Text, Is.EqualTo("Muppets"));
        }

        [Test]
        public void StyledExpanderIsExpanded()
        {
            var tab4 = MainWindow.MainTabControl.Tab4;
            tab4.Click();
            var expander = tab4.Muppets4Expander;
            expander.AssertThat(x => x.IsExpanded, Is.True);
        }

        [Test]
        public void StyledExpanderIsClosed()
        {
            var tab4 = MainWindow.MainTabControl.Tab4;
            tab4.Click();
            var expander = tab4.Muppets4Expander;
            expander.ExpandButton<WpfFrameworkElement>().Click();
            expander.AssertThat(x => x.IsExpanded, Is.False);
        }
    }
}