using System.Linq;
using ruibarbo.core.Wpf.Base;

namespace ruibarbo.core.Wpf.Helpers
{
    public static class WpfItemsControlBaseExtensions
    {
        public static int Count<TNativeElement>(this WpfItemsControlBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ItemsControl
        {
            // TODO? Introduce IItemsControlItem interface?
            return me.AllItems<WpfFrameworkElement>().Count();
        }
    }
}