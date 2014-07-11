using tungsten.core.ElementFactory;

namespace tungsten.core.Wpf.Base
{
    public class WpfContentPresenterBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ContentPresenter
    {
        public WpfContentPresenterBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}