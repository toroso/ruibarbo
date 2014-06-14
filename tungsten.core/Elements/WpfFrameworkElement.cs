namespace tungsten.core.Elements
{
    public class WpfFrameworkElement
        : WpfFrameworkElementBase<System.Windows.FrameworkElement>, IRegisteredElement<System.Windows.FrameworkElement>
    {
        public WpfFrameworkElement(SearchSourceElement searchParent, System.Windows.FrameworkElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}