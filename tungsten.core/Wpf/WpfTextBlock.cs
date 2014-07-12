using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public sealed class WpfTextBlock : WpfTextBlockBase<System.Windows.Controls.TextBlock>, IRegisteredElement
    {
        public WpfTextBlock(ISearchSourceElement searchParent, System.Windows.Controls.TextBlock textBlock)
            : base(searchParent, textBlock)
        {
        }
    }
}