﻿using tungsten.core.ElementFactory;
using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.sampletest.AutomationLayer
{
    public class MainWindow : WpfWindowBase<System.Windows.Window>, IRegisteredElement
    {
        public MainWindow(ISearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public MainTabControl MainTabControl
        {
            get { return this.FindFirstChild<MainTabControl>(By.Name("MainTabs")); }
        }
    }
}