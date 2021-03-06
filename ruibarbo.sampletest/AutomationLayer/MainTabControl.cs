﻿using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    [RegisteredElement]
    public class MainTabControl : WpfTabControlBase<System.Windows.Controls.TabControl>
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