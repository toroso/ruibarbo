using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class Tab23Control : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement<System.Windows.Controls.TabItem>
    {
        public Tab23Control(SearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfTextBox TextBox
        {
            get { return this.FindFirstChild<WpfTextBox>(By.Name("TxtInVirtual")); }
        }
    }
}