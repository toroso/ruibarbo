using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfToggleButton : WpfToggleButtonBase<System.Windows.Controls.Primitives.ToggleButton>
    {
        public WpfToggleButton(ISearchSourceElement searchParent, System.Windows.Controls.Primitives.ToggleButton frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}