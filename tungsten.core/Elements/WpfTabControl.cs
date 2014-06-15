namespace tungsten.core.Elements
{
    public class WpfTabControl : WpfTabControlBase<System.Windows.Controls.TabControl>, IRegisteredElement<System.Windows.Controls.TabControl>
    {
        public WpfTabControl(SearchSourceElement searchParent, System.Windows.Controls.TabControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}