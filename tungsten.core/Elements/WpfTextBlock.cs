using tungsten.core.BaseElements;

namespace tungsten.core.Elements
{
    public class WpfTextBlock : WpfTextBlockBase<System.Windows.Controls.TextBlock>, IRegisteredElement<System.Windows.Controls.TextBlock>
    {
        public WpfTextBlock(ISearchSourceElement searchParent, System.Windows.Controls.TextBlock textBlock)
            : base(searchParent, textBlock)
        {
        }
    }
}