using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfItemsControl : WpfItemsControlBase<System.Windows.Controls.ItemsControl>, IRegisteredElement
    {
        public WpfItemsControl(ISearchSourceElement searchParent, System.Windows.Controls.ItemsControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}