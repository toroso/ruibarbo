using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfComboBoxItem : WpfComboBoxItemBase<System.Windows.Controls.ComboBoxItem>, IRegisteredElement
    {
        public WpfComboBoxItem(ISearchSourceElement searchParent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}