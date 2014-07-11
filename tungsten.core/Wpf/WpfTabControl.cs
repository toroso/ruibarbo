using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfTabControl : WpfTabControlBase<System.Windows.Controls.TabControl>, IRegisteredElement
    {
        public WpfTabControl(ISearchSourceElement searchParent, System.Windows.Controls.TabControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}