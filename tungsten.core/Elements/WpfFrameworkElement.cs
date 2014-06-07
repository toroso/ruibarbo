namespace tungsten.core.Elements
{
    public class WpfFrameworkElement
        : WpfElement<System.Windows.FrameworkElement>
    {
        public WpfFrameworkElement(SearchSourceElement searchParent, System.Windows.FrameworkElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}