using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfTextBlock : WpfTextBlockBase<System.Windows.Controls.TextBlock>, IRegisteredElement<System.Windows.Controls.TextBlock>
    {
        public WpfTextBlock(ISearchSourceElement searchParent, System.Windows.Controls.TextBlock textBlock)
            : base(searchParent, textBlock)
        {
        }
    }
}