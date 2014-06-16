using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class Tab1Control : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement<System.Windows.Controls.TabItem>
    {
        public Tab1Control(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
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