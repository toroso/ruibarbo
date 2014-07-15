using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    public sealed class WpfButton : WpfButtonBase<System.Windows.Controls.Button>, IRegisteredElement
    {
        public WpfButton(ISearchSourceElement searchParent, System.Windows.Controls.Button frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}