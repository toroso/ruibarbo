using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using tungsten.core.Elements;
using tungsten.core.Search;
using tungsten.core.Utils;

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
            get { return this.FindFirstTabItem<Tab1Control>(By.Name("Tab1")); }
        }

        public Tab23Control Tab2
        {
            get { return this.FindFirstTabItem<Tab23Control>(By.Name("Tab2")); }
        }

        public Tab23Control Tab3
        {
            get { return this.FindFirstTabItem<Tab23Control>(By.Name("Tab3")); }
        }
    }
}