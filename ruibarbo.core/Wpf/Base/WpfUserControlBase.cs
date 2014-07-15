using ruibarbo.core.ElementFactory;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfUserControlBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.UserControl
    {
        public WpfUserControlBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}