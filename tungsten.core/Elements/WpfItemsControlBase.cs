using System.Collections.Generic;
using System.Linq;
using System.Text;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfItemsControlBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ItemsControl
    {
        public WpfItemsControlBase(SearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public ItemsControlItemsWrapper<TNativeElement> Items()
        {
            return new ItemsControlItemsWrapper<TNativeElement>(this);
        }
    }

    public static class WpfItemsControlBaseExtensions
    {
        public static IEnumerable<TWpfItem> AllItems<TNativeElement, TWpfItem>(this WpfItemsControlBase<TNativeElement> me, params By[] bys)
            where TNativeElement : System.Windows.Controls.ItemsControl
            where TWpfItem : UntypedWpfElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<object>()
                .SelectMany(item => CreateWpfItem(item, me))
                .OfType<TWpfItem>();
        }

        public static TWpfItem FindFirstItem<TNativeElement, TWpfItem>(this WpfItemsControlBase<TNativeElement> me, params By[] bys)
            where TNativeElement : System.Windows.Controls.ItemsControl
            where TWpfItem : UntypedWpfElement
        {
            var found = me.TryFindFirstItem<TNativeElement, TWpfItem>(bys);
            if (found == null)
            {
                var sb = new StringBuilder();
                var allItems = Invoker.Get(me, frameworkElement => frameworkElement.Items).Cast<object>();
                foreach (var item in allItems)
                {
                    IEnumerable<UntypedWpfElement> wpfElements = ElementFactory.ElementFactory.CreateWpfElements(me, item).ToArray();
                    var matchingTypes = wpfElements.Select(t => t.GetType().Name).Join(", ");
                    var wpfElement = wpfElements.FirstOrDefault(); // Any will do
                    sb.AppendLine(string.Format("   {0} <{1}>", wpfElement.ControlIdentifier(), matchingTypes));
                }

                throw ManglaException.FindFailed("Item", me, bys, sb.ToString());
            }

            return found;
        }

        public static TWpfItem TryFindFirstItem<TNativeElement, TWpfItem>(this WpfItemsControlBase<TNativeElement> me, params By[] bys)
            where TNativeElement : System.Windows.Controls.ItemsControl
            where TWpfItem : UntypedWpfElement
        {
            return me.AllItems<TNativeElement, TWpfItem>(bys).FirstOrDefault(item => bys.All(by => by.Matches(item)));
        }

        private static IEnumerable<UntypedWpfElement> CreateWpfItem<TNativeParent>(object item, WpfItemsControlBase<TNativeParent> parent)
            where TNativeParent : System.Windows.Controls.ItemsControl
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item);
        }
    }
}