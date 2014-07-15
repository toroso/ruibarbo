using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
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