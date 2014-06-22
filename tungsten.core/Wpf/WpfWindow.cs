using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfWindow : WpfWindowBase<System.Windows.Window>, IRegisteredElement<System.Windows.Window>
    {
        public WpfWindow(ISearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
            Invoker.Invoke(this, fe => fe.Activate());
        }
    }
}