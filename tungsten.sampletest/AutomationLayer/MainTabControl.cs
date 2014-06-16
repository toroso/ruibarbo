using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class MainTabControl : WpfTabControlBase<System.Windows.Controls.TabControl>, IRegisteredElement<System.Windows.Controls.TabControl>
    {
        public MainTabControl(ISearchSourceElement searchParent, System.Windows.Controls.TabControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public Tab1Control Tab1
        {
            get { return Items().FindFirst<Tab1Control>(By.Name("Tab1")); }
        }

        public Tab23Control Tab2
        {
            get { return Items().FindFirst<Tab23Control>(By.Name("Tab2")); }
        }

        public Tab23Control Tab3
        {
            get { return Items().FindFirst<Tab23Control>(By.Name("Tab3")); }
        }
    }
}