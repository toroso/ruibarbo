using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfItemsControl : WpfItemsControlBase<System.Windows.Controls.ItemsControl>
    {
        public WpfItemsControl(ISearchSourceElement searchParent, System.Windows.Controls.ItemsControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}