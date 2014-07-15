using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    public sealed class WpfTextBlock : WpfTextBlockBase<System.Windows.Controls.TextBlock>, IRegisteredElement
    {
        public WpfTextBlock(ISearchSourceElement searchParent, System.Windows.Controls.TextBlock textBlock)
            : base(searchParent, textBlock)
        {
        }
    }
}