namespace tungsten.core.Elements
{
    public class WpfFrameworkElement
        : WpfElement<System.Windows.FrameworkElement>
    {
        public WpfFrameworkElement(SearchSourceElement parent, System.Windows.FrameworkElement frameworkElement)
            : base(parent, frameworkElement)
        {
        }
    }
}