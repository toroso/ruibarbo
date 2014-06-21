using tungsten.core.BaseElements;

namespace tungsten.core.Elements
{
    public class WpfTabItem : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement<System.Windows.Controls.TabItem>
    {
        public WpfTabItem(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}