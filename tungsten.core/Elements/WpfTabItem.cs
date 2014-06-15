namespace tungsten.core.Elements
{
    public class WpfTabItem : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement<System.Windows.Controls.TabItem>
    {
        public WpfTabItem(SearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}