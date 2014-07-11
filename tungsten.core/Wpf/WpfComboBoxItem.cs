using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

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