using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfToggleButtonBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.Primitives.ToggleButton
    {
        public WpfToggleButtonBase(ISearchSourceElement searchParent, TNativeElement checkBox)
            : base(searchParent, checkBox)
        {
        }
    }

    public static class WpfToggleButtonBaseExtensions
    {
        public static bool? IsChecked<TNativeElement>(this WpfToggleButtonBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.Primitives.ToggleButton
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsChecked);
        }
    }
}