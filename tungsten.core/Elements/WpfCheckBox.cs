namespace tungsten.core.Elements
{
    public class WpfCheckBox : WpfElement<System.Windows.Controls.CheckBox>
    {
        public WpfCheckBox(SearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }

        public bool? IsChecked
        {
            get { return Get(frameworkElement => frameworkElement.IsChecked); }
        }
    }
}