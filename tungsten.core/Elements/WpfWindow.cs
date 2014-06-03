namespace tungsten.core.Elements
{
    public class WpfWindow : WpfElement<System.Windows.Window>
    {
        public WpfWindow(SearchSourceElement parent, System.Windows.Window frameworkElement)
            : base(parent, frameworkElement)
        {
            Invoke(fe => fe.Activate());
        }
    }
}