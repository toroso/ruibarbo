namespace tungsten.core.Wpf.Base
{
    public class WpfContentControlBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>, IContentControl
        where TNativeElement : System.Windows.Controls.ContentControl
    {
        public WpfContentControlBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public object Content()
        {
            return Invoker.Get(this, frameworkElement => frameworkElement.Content);
        }
    }
}