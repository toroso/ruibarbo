using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfTabItem : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement
    {
        public WpfTabItem(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}