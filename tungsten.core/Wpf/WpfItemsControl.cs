using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfItemsControl : WpfItemsControlBase<System.Windows.Controls.ItemsControl>, IRegisteredElement<System.Windows.Controls.ItemsControl>
    {
        public WpfItemsControl(ISearchSourceElement searchParent, System.Windows.Controls.ItemsControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}