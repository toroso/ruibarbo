using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    [RegisteredElement]
    public class Tab4Control : WpfTabItemBase<System.Windows.Controls.TabItem>
    {
        public Tab4Control(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public Muppets4Expander Muppets4Expander
        {
            get { return this.FindFirstChild<Muppets4Expander>(By.Name("ExpMuppets4")); }
        }
    }
}