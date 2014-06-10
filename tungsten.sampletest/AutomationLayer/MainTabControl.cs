using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class MainTabControl : WpfTabControl
    {
        public MainTabControl(SearchSourceElement searchParent, TabControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public Tab1Control Tab1
        {
            get { return this.TabItems<Tab1Control>(By.Name("Tab1")).First(); }
        }
    }
}