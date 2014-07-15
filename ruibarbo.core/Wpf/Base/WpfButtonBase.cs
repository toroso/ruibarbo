using ruibarbo.core.ElementFactory;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfButtonBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.Primitives.ButtonBase
    {
        public WpfButtonBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}