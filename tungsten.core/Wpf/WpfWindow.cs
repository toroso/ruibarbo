using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;
using tungsten.core.Wpf.Invoker;

namespace tungsten.core.Wpf
{
    public sealed class WpfWindow : WpfWindowBase<System.Windows.Window>, IRegisteredElement
    {
        public WpfWindow(ISearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
            OnUiThread.Invoke(this, fe => fe.Activate());
        }
    }
}