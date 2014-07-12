using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public sealed class WpfButton : WpfButtonBase<System.Windows.Controls.Button>, IRegisteredElement
    {
        public WpfButton(ISearchSourceElement searchParent, System.Windows.Controls.Button frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}