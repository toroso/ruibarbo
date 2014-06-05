namespace tungsten.core.Elements
{
    public class WpfComboBoxItem : WpfElement<System.Windows.Controls.ComboBoxItem>
    {
        public WpfComboBoxItem(SearchSourceElement parent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(parent, frameworkElement)
        {
        }

        public object Content
        {
            get { return Get(frameworkElement => frameworkElement.Content); }
        }
    }
}