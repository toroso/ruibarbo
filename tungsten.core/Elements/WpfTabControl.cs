using System.Collections.Generic;
using System.Linq;
using System.Text;
using tungsten.core.Search;
using tungsten.core.Utils;

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

        /// <summary>
        /// Get all TabItems. Note that this method returns all possible WpfElement representations of a TabItem so there might
        /// be more items returned than there are actual tabs.
        /// TODO: Is this really useful? Perhaps a FindAllTabItems() of type and by condition. Must only return one instance per TabItem though.
        /// </summary>
        public static IEnumerable<WpfTabItem> TabItems(this WpfTabControl me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<System.Windows.Controls.TabItem>()
                .SelectMany(item => CreateWpfTabItem(item, me));
        }

        public static TWpfTabItem FindFirstTabItem<TWpfTabItem>(this WpfTabControl me, params By[] bys)
            where TWpfTabItem : WpfTabItem
        {
            var found = me.TryFindFirstTabItem<TWpfTabItem>(bys);
            if (found == null)
            {
                var sb = new StringBuilder();
                var allTabItems = Invoker.Get(me, frameworkElement => frameworkElement.Items)
                    .Cast<System.Windows.Controls.TabItem>();
                foreach (var tabItem in allTabItems)
                {
                    IEnumerable<WpfTabItem> wpfElements = ElementFactory.ElementFactory.CreateWpfElements(me, tabItem)
                        .OfType<WpfTabItem>()
                        .ToArray();
                    var matchingTypes = wpfElements.Select(t => t.GetType().Name).Join(", ");
                    var wpfElement = wpfElements.FirstOrDefault(); // Any will do
                    sb.AppendLine(string.Format("   {0} <{1}>", wpfElement.ControlIdentifier(), matchingTypes));
                }

                throw ManglaException.FindFailed("TabItem", me, bys, sb.ToString());
            }

            return found;
        }

        public static TWpfTabItem TryFindFirstTabItem<TWpfTabItem>(this WpfTabControl me, params By[] bys)
            where TWpfTabItem : WpfTabItem
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<System.Windows.Controls.TabItem>()
                .SelectMany(item => CreateWpfTabItem(item, me))
                .OfType<TWpfTabItem>() // OfType? Or OfExcactType? TODO: Preferrably the latter (although practically it doesn't matter).
                .FirstOrDefault(item => bys.All(by => by.Matches(item)));
        }

        private static IEnumerable<WpfTabItem> CreateWpfTabItem(System.Windows.Controls.TabItem item, WpfTabControl parent)
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item).OfType<WpfTabItem>();
        }
    }
}