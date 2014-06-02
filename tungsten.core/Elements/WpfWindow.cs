namespace tungsten.core.Elements
{
    public class WpfWindow : WpfElement<System.Windows.Window>
    {
        public WpfWindow(SearchSourceElement parent, System.Windows.Window frameworkElement)
            : base(parent, frameworkElement)
        {
            var strongReference = GetFrameworkElement();
            Invoker.Invoke(() => strongReference.Activate());
        }
    }
}