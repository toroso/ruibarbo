namespace tungsten.core.Elements
{
    public class WpfUserControl : WpfElement<System.Windows.Controls.UserControl>
    {
        public WpfUserControl(SearchSourceElement searchParent, System.Windows.Controls.UserControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}