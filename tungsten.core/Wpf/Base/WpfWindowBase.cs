namespace tungsten.core.Wpf.Base
{
    public class WpfWindowBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Window
    {
        public WpfWindowBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
            MakeSureWindowIsTopmost();
        }

        private void MakeSureWindowIsTopmost()
        {
            Invoker.Invoke(this, fe => fe.Activate());
        }
    }
}