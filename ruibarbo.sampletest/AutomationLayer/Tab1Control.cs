using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    [RegisteredElement]
    public class Tab1Control : WpfTabItemBase<System.Windows.Controls.TabItem>
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