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
            var selectedItem = Invoker.Get(me, frameworkElement => (System.Windows.Controls.TabItem)frameworkElement.SelectedItem);
            return CreateWpfTabItem(selectedItem, me);
        }

        public static IEnumerable<WpfTabItem> TabItems(this WpfTabControl me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<System.Windows.Controls.TabItem>()
                .Select(item => CreateWpfTabItem(item, me))
                .ToArray();
        }

        private static WpfTabItem CreateWpfTabItem(System.Windows.Controls.TabItem item, WpfTabControl parent)
        {
            return (WpfTabItem)ElementFactory.ElementFactory.CreateWpfElement(parent, item);
        }
    }
}