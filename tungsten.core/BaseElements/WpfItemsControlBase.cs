using System.Collections.Generic;
using System.Linq;
using System.Text;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.BaseElements
{
    public class WpfItemsControlBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ItemsControl
    {
        public WpfItemsControlBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public TWpfItem FindFirstItem<TWpfItem>(params By[] bys)
            where TWpfItem : class, ISearchSourceElement
        {
            var found = TryFindFirstItem<TWpfItem>(bys);
            if (found == null)
            {
                var sb = new StringBuilder();
                var allItems = Invoker.Get(this, frameworkElement => frameworkElement.Items).Cast<object>();
                foreach (var item in allItems)
                {
                    IEnumerable<ISearchSourceElement> wpfElements = ElementFactory.ElementFactory.CreateWpfElements(this, item).ToArray();
                    var matchingTypes = wpfElements.Select(t => t.GetType().Name).Join(", ");
                    var wpfElement = wpfElements.FirstOrDefault(); // Any will do
                    sb.AppendLine(string.Format("   {0} <{1}>", wpfElement.ControlIdentifier(), matchingTypes));
                }

                throw ManglaException.FindFailed("Item", this, bys, sb.ToString());
            }

            return found;
        }

        public TWpfItem TryFindFirstItem<TWpfItem>(params By[] bys)
            where TWpfItem : ISearchSourceElement
        {
            return AllItems<TWpfItem>(bys).FirstOrDefault(item => bys.All(by => by.Matches(item)));
        }

        public IEnumerable<TWpfItem> AllItems<TWpfItem>(params By[] bys)
            where TWpfItem : ISearchSourceElement
        {
            return Invoker.Get(this, frameworkElement => frameworkElement.Items)
                .Cast<object>()
                .SelectMany(item => ElementFactory.ElementFactory.CreateWpfElements(this, item))
                .OfType<TWpfItem>();
        }
    }
}