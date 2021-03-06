﻿using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    [RegisteredElement]
    public class MainWindow : WpfWindowBase<System.Windows.Window>
    {
        public MainWindow(ISearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public MainTabControl MainTabControl
        {
            get { return this.FindFirstChild<MainTabControl>(By.Name("MainTabs")); }
        }

        public WpfImage SpaceImage
        {
            get { return this.FindFirstChild<WpfImage>(By.Name("SpaceImage")); }
        }
    }
}