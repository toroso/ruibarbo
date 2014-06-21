namespace tungsten.core.BaseElements
{
    public class WpfCheckBoxBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.CheckBox
    {
        public WpfCheckBoxBase(ISearchSourceElement searchParent, TNativeElement checkBox)
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