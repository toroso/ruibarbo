using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfWindowBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Window
    {
        public WpfWindowBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
            MakeSureWindowIsTopmost();
        }

        private void MakeSureWindowIsTopmost()
        {
            OnUiThread.Invoke(this, fe => fe.Activate());
        }
    }
}