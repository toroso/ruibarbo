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

        public WpfCheckBox ShowStuffCheckBox
        {
            get { return this.FindFirstChild<WpfCheckBox>(By.Name("ShowStuff")); }
        }

        public StuffControl StuffControl
        {
            get { return this.FindFirstChild<StuffControl>(By.Name("CtrlStuff")); }
        }
    }
}