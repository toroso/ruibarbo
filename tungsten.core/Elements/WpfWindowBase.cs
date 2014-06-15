namespace tungsten.core.Elements
{
    public class WpfWindowBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Window
    {
        public WpfWindowBase(SearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
            Invoker.Invoke(this, fe => fe.Activate());
        }
    }
}