using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;

namespace tungsten.sampletest.AutomationLayer
{
    public class Muppets4Expander : WpfExpanderBase<System.Windows.Controls.Expander>, IRegisteredElement
    {
        public Muppets4Expander(ISearchSourceElement searchParent, System.Windows.Controls.Expander frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfItemsControl MuppetsItemsControl
        {
            get { return this.FindFirstChild<WpfItemsControl>(By.Name("Muppets")); }
        }

        public override TElement ExpandButton<TElement>()
        {
            return this.FindFirstChild<TElement>(By.Name("ExpanderToggle"));
        }
    }
}