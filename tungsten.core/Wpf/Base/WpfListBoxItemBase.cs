using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Invoker;

namespace tungsten.core.Wpf.Base
{
    public class WpfListBoxItemBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ListBoxItem
    {
        public WpfListBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfListBoxItemBaseExtensions
    {
        public static bool IsSelected<TNativeElement>(this WpfListBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ListBoxItem
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsSelected);
        }
    }
}