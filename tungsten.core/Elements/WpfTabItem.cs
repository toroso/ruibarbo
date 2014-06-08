namespace tungsten.core.Elements
{
    public class WpfTabItem : WpfElement<System.Windows.Controls.TabItem>
    {
        public WpfTabItem(SearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfTabItemExtensions
    {
        public static object Header(this WpfTabItem me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Header);
        }

        public static bool IsSelected(this WpfTabItem me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsSelected);
        }
    }
}