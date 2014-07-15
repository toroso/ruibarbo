using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfWindow : WpfWindowBase<System.Windows.Window>
    {
        public WpfWindow(ISearchSourceElement searchParent, System.Windows.Window frameworkElement)
            : base(searchParent, frameworkElement)
        {
            OnUiThread.Invoke(this, fe => fe.Activate());
        }
    }
}