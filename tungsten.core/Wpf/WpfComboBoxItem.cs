using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfComboBoxItem : WpfComboBoxItemBase<System.Windows.Controls.ComboBoxItem>, IRegisteredElement<System.Windows.Controls.ComboBoxItem>
    {
        public WpfComboBoxItem(ISearchSourceElement searchParent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}