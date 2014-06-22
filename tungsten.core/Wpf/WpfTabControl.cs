using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfTabControl : WpfTabControlBase<System.Windows.Controls.TabControl>, IRegisteredElement<System.Windows.Controls.TabControl>
    {
        public WpfTabControl(ISearchSourceElement searchParent, System.Windows.Controls.TabControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}