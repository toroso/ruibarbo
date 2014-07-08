using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfExpander : WpfExpanderBase<System.Windows.Controls.Expander>, IRegisteredElement
    {
        public WpfExpander(ISearchSourceElement searchParent, System.Windows.Controls.Expander frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}