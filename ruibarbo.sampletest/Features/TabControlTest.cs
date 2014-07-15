using System.Linq;
using NUnit.Framework;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.nunit;
using ruibarbo.sampletest.AutomationLayer;

namespace ruibarbo.sampletest.Features
{
    [TestFixture]
    public class TabControlTest : TestBase
    {
        [Test]
        public void TabItemCount()
        {
            var mainTabControl = MainWindow.MainTabControl;
            mainTabControl.AssertThat(x => x.AllItems<WpfTabItem>().Count(), Is.EqualTo(5));
        }

        [Test]
        public void FirstTabItemIsSelected()
        {
            var mainTabControl = MainWindow.MainTabControl;
            var tab1 = mainTabControl.AllItems<WpfTabItem>().First();
            tab1.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void TabControlHasFirstTabItemAsSelected()
        {
            var mainTabControl = MainWindow.MainTabControl;
            var tab1 = mainTabControl.AllItems<WpfTabItem>().First(x => x.Header().Equals("Tab 1"));
            tab1.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void ChangeToSecondTabItem()
        {
            var mainTabControl = MainWindow.MainTabControl;
            var tab2 = mainTabControl.AllItems<WpfTabItem>().First(x => x.Header().Equals("Tab 2"));
            tab2.Click();
            tab2.AssertThat(x => x.IsSelected(), Is.True);
        }

        [Test]
        public void VirtualizedTabControlBehavior()
        {
            // Not really sure what I'm testing here...
            var mainTabControl = MainWindow.MainTabControl;

            var tab2 = mainTabControl.Tab2;
            tab2.Click();
            var wpfTextBox2 = tab2.TextBox;
            wpfTextBox2.AssertThat(x => x.IsVisible, Is.True);

            var tab3 = mainTabControl.Tab3;
            tab3.Click();
            wpfTextBox2.AssertThat(x => x.IsVisible, Is.False);

            tab2.Click();
            wpfTextBox2.AssertThat(x => x.IsVisible, Is.True);

            var tab1 = mainTabControl.Tab1;
            tab1.Click();
            wpfTextBox2.AssertThat(x => x.IsVisible, Is.False);
        }
    }
}