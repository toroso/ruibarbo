using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfCheckBoxBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.CheckBox
    {
        public WpfCheckBoxBase(ISearchSourceElement searchParent, TNativeElement checkBox)
            : base(searchParent, checkBox)
        {
        }

        public bool? IsChecked
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsChecked); }
        }
    }
}