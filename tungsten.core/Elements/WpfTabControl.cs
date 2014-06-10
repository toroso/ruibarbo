using System.Collections.Generic;
using System.Linq;
using tungsten.core.Search;

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
            // TODO: What if selectedItem is null?
            var selectedItem = Invoker.Get(me, frameworkElement => (System.Windows.Controls.TabItem)frameworkElement.SelectedItem);
            return CreateWpfTabItem(selectedItem, me).First();
        }

        public static IEnumerable<WpfTabItem> TabItems(this WpfTabControl me)
        {
            return me.TabItems<WpfTabItem>();
        }

        public static IEnumerable<TWpfTabItem> TabItems<TWpfTabItem>(this WpfTabControl me, params By[] bys)
            where TWpfTabItem : WpfTabItem
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<System.Windows.Controls.TabItem>()
                .SelectMany(item => CreateWpfTabItem(item, me))
                .OfType<TWpfTabItem>()
                .Where(item => bys.All(by => by.Matches(item)));
        }

        private static IEnumerable<WpfTabItem> CreateWpfTabItem(System.Windows.Controls.TabItem item, WpfTabControl parent)
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item).OfType<WpfTabItem>();
        }
    }
}