using tungsten.core.BaseElements;

namespace tungsten.core.Elements
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