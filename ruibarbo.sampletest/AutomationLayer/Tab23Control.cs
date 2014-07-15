using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    [RegisteredElement]
    public class Tab23Control : WpfTabItemBase<System.Windows.Controls.TabItem>
    {
        public Tab23Control(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfTextBox TextBox
        {
            get { return this.FindFirstChild<WpfTextBox>(By.Name("TxtInVirtual")); }
        }
    }
}