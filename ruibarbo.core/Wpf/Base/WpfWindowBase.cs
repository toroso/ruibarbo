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
        }

        public void MakeSureWindowIsTopmost()
        {
            OnUiThread.Invoke(this, frameworkElement => frameworkElement.Activate());
        }

        public string Title
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.Title); }
        }

        public void MoveTo(int x, int y)
        {
            OnUiThread.Invoke(this, frameworkElement =>
                {
                    frameworkElement.Left = x;
                    frameworkElement.Top = y;
                });
        }
    }
}