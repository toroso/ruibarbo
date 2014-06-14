namespace tungsten.core.Elements
{
    public class WpfCheckBox : WpfElement<System.Windows.Controls.CheckBox>, IRegisteredElement<System.Windows.Controls.CheckBox>
    {
        public WpfCheckBox(SearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }
    }

    public static class WpfCheckBoxExtensions
    {
        public static bool? IsChecked(this WpfCheckBox me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsChecked);
        }
    }
}