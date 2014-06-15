using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public class ItemsControlItemsWrapper<TNativeElement>
        where TNativeElement : System.Windows.Controls.TabControl
    {
        private readonly WpfTabControlBase<TNativeElement> _element;

        public ItemsControlItemsWrapper(WpfTabControlBase<TNativeElement> element)
        {
            _element = element;
        }

        public TWpfTabItem FindFirst<TWpfTabItem>(params By[] bys)
            where TWpfTabItem : UntypedWpfElement
        {
            return _element.FindFirstTabItem<TNativeElement, TWpfTabItem>(bys);
        }
    }
}