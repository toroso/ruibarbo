using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class MainWindow : WpfWindow
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