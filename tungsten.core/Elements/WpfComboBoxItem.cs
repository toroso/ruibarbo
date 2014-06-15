namespace tungsten.core.Elements
{
    public class WpfComboBoxItem : WpfComboBoxItemBase<System.Windows.Controls.ComboBoxItem>, IRegisteredElement<System.Windows.Controls.ComboBoxItem>
    {
        public WpfComboBoxItem(SearchSourceElement searchParent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}