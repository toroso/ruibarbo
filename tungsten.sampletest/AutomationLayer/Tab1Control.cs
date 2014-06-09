using System.Windows.Controls;
using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class Tab1Control : WpfTabItem
    {
        public Tab1Control(SearchSourceElement searchParent, TabItem frameworkElement)
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