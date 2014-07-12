using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public sealed class WpfTabItem : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement
    {
        public WpfTabItem(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}