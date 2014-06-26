using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfButton : WpfButtonBase<System.Windows.Controls.Button>, IRegisteredElement
    {
        public WpfButton(ISearchSourceElement searchParent, System.Windows.Controls.Button frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}