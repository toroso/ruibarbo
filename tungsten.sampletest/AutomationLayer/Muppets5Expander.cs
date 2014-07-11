using tungsten.core.ElementFactory;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.sampletest.AutomationLayer
{
    public class Muppets5Expander : WpfExpanderBase<System.Windows.Controls.Expander>, IRegisteredElement
    {
        public Muppets5Expander(ISearchSourceElement searchParent, System.Windows.Controls.Expander frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfListBox MuppetsListBox
        {
            get { return this.FindFirstChild<WpfListBox>(By.Name("LstMuppets")); }
        }
    }
}