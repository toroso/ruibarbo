using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public sealed class WpfComboBoxItem : WpfComboBoxItemBase<System.Windows.Controls.ComboBoxItem>, IRegisteredElement
    {
        public WpfComboBoxItem(ISearchSourceElement searchParent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}