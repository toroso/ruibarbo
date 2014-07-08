using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;

namespace tungsten.sampletest.AutomationLayer
{
    public class MuppetsExpander : WpfExpanderBase<System.Windows.Controls.Expander>, IRegisteredElement
    {
        public MuppetsExpander(ISearchSourceElement searchParent, System.Windows.Controls.Expander frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfListBox MuppetsListBox
        {
            get { return this.FindFirstChild<WpfListBox>(By.Name("LstMuppets")); }
        }
    }
}