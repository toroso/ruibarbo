using System.Collections.Generic;
using System.Linq;
using System.Text;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfTabControlBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TabControl
    {
        public WpfTabControlBase(SearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public ItemsControlItemsWrapper<TNativeElement> Items()
        {
            return new ItemsControlItemsWrapper<TNativeElement>(this);
        }
    }

    public static class WpfTabControlBaseExtensions
    {
        // TODO: Return IWpfItem? Or UntypedElement And also, a type safe version in ItemsControlItemsWrapper.
        // TODO (Updated): Remove. Only support IsSelected on WpfTabItemBase.
        public static WpfTabItem SelectedItem<TNativeElement>(this WpfTabControlBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TabControl
        {
            // TODO: What if selectedItem is null?
            var selectedItem = Invoker.Get(me, frameworkElement => frameworkElement.SelectedItem);
            return CreateWpfTabItem(selectedItem, me)
                .OfType<WpfTabItem>()
                .First();
        }

        /// <summary>
        /// Get all TabItems. Note that this method returns all possible WpfFrameworkElementBase representations of a TabItem so there might
        /// be more items returned than there are actual tabs.
        /// TODO: Is this really useful? Perhaps a FindAllTabItems() of type and by condition. Must only return one instance per TabItem though.
        /// </summary>
        public static IEnumerable<WpfTabItem> TabItems<TNativeElement>(this WpfTabControlBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TabControl
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<object>()
                .SelectMany(item => CreateWpfTabItem(item, me))
                .OfType<WpfTabItem>();
        }

        public static TWpfTabItem FindFirstTabItem<TNativeElement, TWpfTabItem>(this WpfTabControlBase<TNativeElement> me, params By[] bys)
            where TNativeElement : System.Windows.Controls.TabControl
            where TWpfTabItem : UntypedWpfElement
        {
            var found = me.TryFindFirstItem<TNativeElement, TWpfTabItem>(bys);
            if (found == null)
            {
                var sb = new StringBuilder();
                var allTabItems = Invoker.Get(me, frameworkElement => frameworkElement.Items).Cast<object>();
                foreach (var tabItem in allTabItems)
                {
                    IEnumerable<UntypedWpfElement> wpfElements = ElementFactory.ElementFactory.CreateWpfElements(me, tabItem).ToArray();
                    var matchingTypes = wpfElements.Select(t => t.GetType().Name).Join(", ");
                    var wpfElement = wpfElements.FirstOrDefault(); // Any will do
                    sb.AppendLine(string.Format("   {0} <{1}>", wpfElement.ControlIdentifier(), matchingTypes));
                }

                throw ManglaException.FindFailed("TabItem", me, bys, sb.ToString());
            }

            return found;
        }

        public static TWpfTabItem TryFindFirstItem<TNativeElement, TWpfTabItem>(this WpfTabControlBase<TNativeElement> me, params By[] bys)
            where TNativeElement : System.Windows.Controls.TabControl
            where TWpfTabItem : UntypedWpfElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<object>()
                .SelectMany(item => CreateWpfTabItem(item, me))
                .OfType<TWpfTabItem>() // OfType? Or OfExcactType? TODO: Preferrably the latter (although practically it doesn't matter).
                .FirstOrDefault(item => bys.All(by => by.Matches(item)));
        }

        private static IEnumerable<UntypedWpfElement> CreateWpfTabItem<TNativeParent>(object item, WpfTabControlBase<TNativeParent> parent)
            where TNativeParent : System.Windows.Controls.TabControl
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item); // .OfType<UntypedWpfElement>();
        }
    }
}