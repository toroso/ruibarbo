using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfTextBlock : WpfTextBlockBase<System.Windows.Controls.TextBlock>, IRegisteredElement
    {
        public WpfTextBlock(ISearchSourceElement searchParent, System.Windows.Controls.TextBlock textBlock)
            : base(searchParent, textBlock)
        {
        }
    }
}