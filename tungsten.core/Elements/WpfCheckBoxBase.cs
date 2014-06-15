namespace tungsten.core.Elements
{
    public class WpfCheckBoxBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.CheckBox
    {
        public WpfCheckBoxBase(SearchSourceElement searchParent, TNativeElement checkBox)
            : base(searchParent, checkBox)
        {
        }
    }

    public static class WpfCheckBoxBaseExtensions
    {
        public static bool? IsChecked<TNativeElement>(this WpfCheckBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.CheckBox
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsChecked);
        }
    }
}