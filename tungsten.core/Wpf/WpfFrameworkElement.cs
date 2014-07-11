using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfFrameworkElement
        : WpfFrameworkElementBase<System.Windows.FrameworkElement>, IRegisteredElement
    {
        public WpfFrameworkElement(ISearchSourceElement searchParent, System.Windows.FrameworkElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}