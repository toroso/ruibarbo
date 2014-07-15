using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfLabelBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.Label
    {
        public WpfLabelBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfLabelBaseExtensions
    {
        public static object Content<TNativeElement>(this WpfLabelBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.Label
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.Content);
        }
    }
}