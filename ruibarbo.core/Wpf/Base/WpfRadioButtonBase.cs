using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfRadioButtonBase<TNativeElement> : WpfButtonBase<TNativeElement> // Actually WpfToggleButtonBase
        where TNativeElement : System.Windows.Controls.RadioButton
    {
        public WpfRadioButtonBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public bool? IsChecked
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsChecked); }
        }
    }
}