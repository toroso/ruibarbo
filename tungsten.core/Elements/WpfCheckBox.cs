namespace tungsten.core.Elements
{
    public class WpfCheckBox : WpfElement<System.Windows.Controls.CheckBox>
    {
        public WpfCheckBox(SearchSourceElement parent, System.Windows.Controls.CheckBox checkBox)
            : base(parent, checkBox)
        {
        }

        public bool? IsChecked
        {
            get { return Get(frameworkElement => frameworkElement.IsChecked); }
        }
    }
}