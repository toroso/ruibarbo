using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfCheckBoxBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.CheckBox
    {
        public WpfCheckBoxBase(ISearchSourceElement searchParent, TNativeElement checkBox)
            : base(searchParent, checkBox)
        {
        }
    }

    public static class WpfCheckBoxBaseExtensions
    {
        public static bool? IsChecked<TNativeElement>(this WpfCheckBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.CheckBox
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsChecked);
        }
    }
}