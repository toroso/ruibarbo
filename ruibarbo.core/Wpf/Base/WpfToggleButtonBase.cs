using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfToggleButtonBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.Primitives.ToggleButton
    {
        public WpfToggleButtonBase(ISearchSourceElement searchParent, TNativeElement checkBox)
            : base(searchParent, checkBox)
        {
        }

        public bool? IsChecked
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsChecked); }
        }
    }
}