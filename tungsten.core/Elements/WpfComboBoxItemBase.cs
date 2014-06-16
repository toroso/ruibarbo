namespace tungsten.core.Elements
{
    public class WpfComboBoxItemBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ComboBoxItem
    {
        public WpfComboBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfComboBoxItemBaseExtensions
    {
        public static object Content<TNativeElement>(this WpfComboBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBoxItem
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Content);
        }

        public static void BringIntoView<TNativeElement>(this WpfComboBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBoxItem
        {
            Invoker.Invoke(me, frameworkElement => frameworkElement.BringIntoView());
        }
    }
}