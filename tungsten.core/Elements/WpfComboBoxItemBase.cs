namespace tungsten.core.Elements
{
    public class WpfComboBoxItemBase<TNativeElement> : WpfContentControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ComboBoxItem
    {
        public WpfComboBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfComboBoxItemBaseExtensions
    {
        public static void BringIntoView<TNativeElement>(this WpfComboBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBoxItem
        {
            Invoker.Invoke(me, frameworkElement => frameworkElement.BringIntoView());
        }
    }
}