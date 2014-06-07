namespace tungsten.core.Elements
{
    public class WpfWindow : WpfElement<System.Windows.Window>
    {
        public WpfWindow(SearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
            Invoke(fe => fe.Activate());
        }
    }
}