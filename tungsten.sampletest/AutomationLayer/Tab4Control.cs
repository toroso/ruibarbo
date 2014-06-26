using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;

namespace tungsten.sampletest.AutomationLayer
{
    public class Tab4Control : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement<System.Windows.Controls.TabItem>
    {
        public Tab4Control(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfItemsControl MuppetsItemsControl
        {
            get { return this.FindFirstChild<WpfItemsControl>(By.Name("Muppets")); }
        }
    }
}