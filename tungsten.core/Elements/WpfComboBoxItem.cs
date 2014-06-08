namespace tungsten.core.Elements
{
    public class WpfComboBoxItem : WpfElement<System.Windows.Controls.ComboBoxItem>
    {
        public WpfComboBoxItem(SearchSourceElement searchParent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfComboBoxItemExtensions
    {
        public static object Content(this WpfComboBoxItem me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Content);
        }

        public static void BringIntoView(this WpfComboBoxItem me)
        {
            Invoker.Invoke(me, frameworkElement => frameworkElement.BringIntoView());
        }
    }
}