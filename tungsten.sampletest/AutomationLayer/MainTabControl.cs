using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf.Base;

namespace tungsten.sampletest.AutomationLayer
{
    public class MainTabControl : WpfTabControlBase<System.Windows.Controls.TabControl>, IRegisteredElement
    {
        public MainTabControl(ISearchSourceElement searchParent, System.Windows.Controls.TabControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public Tab1Control Tab1
        {
            get { return FindFirstItem<Tab1Control>(By.Name("Tab1")); }
        }

        public Tab23Control Tab2
        {
            get { return FindFirstItem<Tab23Control>(By.Name("Tab2")); }
        }

        public Tab23Control Tab3
        {
            get { return FindFirstItem<Tab23Control>(By.Name("Tab3")); }
        }

        public Tab4Control Tab4
        {
            get { return FindFirstItem<Tab4Control>(By.Name("Tab4")); }
        }

        public Tab5Control Tab5
        {
            get { return FindFirstItem<Tab5Control>(By.Name("Tab5")); }
        }
    }
}