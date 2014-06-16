using System.Collections.Generic;
using System.Linq;

namespace tungsten.core.Elements
{
    public class WpfTabControlBase<TNativeElement> : WpfItemsControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TabControl
    {
        public WpfTabControlBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
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

        private static IEnumerable<UntypedWpfElement> CreateWpfTabItem<TNativeParent>(object item, WpfTabControlBase<TNativeParent> parent)
            where TNativeParent : System.Windows.Controls.TabControl
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item); // .OfType<UntypedWpfElement>();
        }
    }
}