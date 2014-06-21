using tungsten.core.BaseElements;

namespace tungsten.core.Elements
{
    public class WpfButton : WpfButtonBase<System.Windows.Controls.Button>, IRegisteredElement<System.Windows.Controls.Button>
    {
        public WpfButton(ISearchSourceElement searchParent, System.Windows.Controls.Button frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}