using System.Windows.Controls;
using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class Tab23Control : WpfTabItem
    {
        public Tab23Control(SearchSourceElement searchParent, TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfTextBox TextBox
        {
            get { return this.FindFirstChild<WpfTextBox>(By.Name("TxtInVirtual")); }
        }
    }
}