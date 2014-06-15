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
    }
    public static class WpfItemsControlBaseExtensions
    {
        public static TWpfTabItem FindFirstItem<TNativeElement, TWpfTabItem>(this WpfItemsControlBase<TNativeElement> me, params By[] bys)
            where TNativeElement : System.Windows.Controls.ItemsControl
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

                throw ManglaException.FindFailed("Item", me, bys, sb.ToString());
            }

            return found;
        }

        public static TWpfTabItem TryFindFirstItem<TNativeElement, TWpfTabItem>(this WpfItemsControlBase<TNativeElement> me, params By[] bys)
            where TNativeElement : System.Windows.Controls.ItemsControl
            where TWpfTabItem : UntypedWpfElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<object>()
                .SelectMany(item => CreateWpfTabItem(item, me))
                .OfType<TWpfTabItem>() // OfType? Or OfExcactType? TODO: Preferrably the latter (although practically it doesn't matter).
                .FirstOrDefault(item => bys.All(by => by.Matches(item)));
        }

        private static IEnumerable<UntypedWpfElement> CreateWpfTabItem<TNativeParent>(object item, WpfItemsControlBase<TNativeParent> parent)
            where TNativeParent : System.Windows.Controls.ItemsControl
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item); // .OfType<UntypedWpfElement>();
        }
    }
}