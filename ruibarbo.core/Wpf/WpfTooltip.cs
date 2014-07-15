using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfTooltip : WpfTooltipBase<System.Windows.Controls.ToolTip>
    {
        public WpfTooltip(ISearchSourceElement searchParent, System.Windows.Controls.ToolTip frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}