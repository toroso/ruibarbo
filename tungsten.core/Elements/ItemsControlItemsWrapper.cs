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

        public TWpfTabItem FindFirst<TWpfTabItem>(params By[] bys)
            where TWpfTabItem : UntypedWpfElement
        {
            return _element.FindFirstItem<TNativeElement, TWpfTabItem>(bys);
        }
    }
}