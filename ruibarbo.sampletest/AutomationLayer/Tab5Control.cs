using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    public class Tab5Control : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement
    {
        public Tab5Control(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public Muppets5Expander Muppets5Expander
        {
            get { return this.FindFirstChild<Muppets5Expander>(By.Name("ExpMuppets5")); }
        }
    }
}