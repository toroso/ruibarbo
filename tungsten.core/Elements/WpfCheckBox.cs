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
            get
            {
                var strongReference = GetFrameworkElement();
                return Invoker.Get(() => strongReference.IsChecked);
            }
        }
    }
}