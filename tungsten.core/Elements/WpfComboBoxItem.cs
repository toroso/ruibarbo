namespace tungsten.core.Elements
{
    public class WpfComboBoxItem : WpfElement<System.Windows.Controls.ComboBoxItem>
    {
        public WpfComboBoxItem(SearchSourceElement searchParent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public object Content
        {
            get { return Get(frameworkElement => frameworkElement.Content); }
        }

        public void BringIntoView()
        {
            Invoke(frameworkElement => frameworkElement.BringIntoView());
        }
    }
}