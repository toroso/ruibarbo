using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    public sealed class WpfExpander : WpfExpanderBase<System.Windows.Controls.Expander>, IRegisteredElement
    {
        public WpfExpander(ISearchSourceElement searchParent, System.Windows.Controls.Expander frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}