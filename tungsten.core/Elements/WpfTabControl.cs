using System.Collections.Generic;
using System.Linq;

namespace tungsten.core.Elements
{
    public class WpfTabControl : WpfElement<System.Windows.Controls.TabControl>
    {
        public WpfTabControl(SearchSourceElement searchParent, System.Windows.Controls.TabControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfTabControlExtensions
    {
        public static WpfTabItem SelectedItem(this WpfTabControl me)
        {
            return Invoker.Get(me, frameworkElement => AsWpfTabItem(frameworkElement.SelectedItem, me));
        }

        public static IEnumerable<WpfTabItem> TabItems(this WpfTabControl me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items
                .Cast<System.Windows.Controls.TabItem>()
                .Select(item => AsWpfTabItem(item, me))
                .ToArray());
        }

        private static WpfTabItem AsWpfTabItem(object item, WpfTabControl parent)
        {
            return new WpfTabItem(parent, (System.Windows.Controls.TabItem)item);
        }
    }
}