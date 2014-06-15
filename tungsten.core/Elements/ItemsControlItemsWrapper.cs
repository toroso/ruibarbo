using System.Collections.Generic;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public class ItemsControlItemsWrapper<TNativeElement>
        where TNativeElement : System.Windows.Controls.ItemsControl
    {
        private readonly WpfItemsControlBase<TNativeElement> _element;

        public ItemsControlItemsWrapper(WpfItemsControlBase<TNativeElement> element)
        {
            _element = element;
        }

        public IEnumerable<TWpfItem> All<TWpfItem>(params By[] bys)
            where TWpfItem : UntypedWpfElement
        {
            return _element.AllItems<TNativeElement, TWpfItem>(bys);
        }

        public TWpfItem FindFirst<TWpfItem>(params By[] bys)
            where TWpfItem : UntypedWpfElement
        {
            return _element.FindFirstItem<TNativeElement, TWpfItem>(bys);
        }

        public TWpfItem TryFindFirst<TWpfItem>(params By[] bys)
            where TWpfItem : UntypedWpfElement
        {
            return _element.TryFindFirstItem<TNativeElement, TWpfItem>(bys);
        }
    }
}