using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfFrameworkElement : WpfFrameworkElementBase<System.Windows.FrameworkElement>
    {
        public WpfFrameworkElement(ISearchSourceElement searchParent, System.Windows.FrameworkElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}