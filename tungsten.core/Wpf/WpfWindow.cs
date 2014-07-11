using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfWindow : WpfWindowBase<System.Windows.Window>, IRegisteredElement
    {
        public WpfWindow(ISearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
            Invoker.Invoke(this, fe => fe.Activate());
        }
    }
}