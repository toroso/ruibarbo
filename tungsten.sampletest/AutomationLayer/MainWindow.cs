﻿using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class MainWindow : WpfWindowBase<System.Windows.Window>, IRegisteredElement<System.Windows.Window>
    {
        public MainWindow(SearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public MainTabControl MainTabControl
        {
            get { return this.FindFirstChild<MainTabControl>(By.Name("MainTabs")); }
        }
    }
}