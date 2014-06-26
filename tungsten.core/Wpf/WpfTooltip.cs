using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfTooltip : WpfTooltipBase<System.Windows.Controls.ToolTip>, IRegisteredElement
    {
        public WpfTooltip(ISearchSourceElement searchParent, System.Windows.Controls.ToolTip frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}