using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfComboBoxItemBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>, IComboBoxItem
        where TNativeElement : System.Windows.Controls.ComboBoxItem
    {
        public WpfComboBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public bool IsSelected
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsSelected); }
        }

        public override object NativeParent
        {
            get { return OnUiThread.Get(this, System.Windows.Controls.ItemsControl.ItemsControlFromItemContainer); }
        }
    }
}