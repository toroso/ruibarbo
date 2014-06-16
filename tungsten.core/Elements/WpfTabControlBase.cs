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

        private static IEnumerable<UntypedWpfElement> CreateWpfTabItem<TNativeParent>(object item, WpfTabControlBase<TNativeParent> parent)
            where TNativeParent : System.Windows.Controls.TabControl
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item);
        }
    }
}