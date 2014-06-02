namespace tungsten.core.Elements
{
    public class WpfCheckBox : WpfElement
    {
        public WpfCheckBox(SearchSourceElement parent, System.Windows.Controls.CheckBox checkBox)
            : base(parent, checkBox)
        {
        }

        public bool? IsChecked
        {
            get
            {
                var strongReference = GetFrameworkElement<System.Windows.Controls.CheckBox>();
                return Invoker.Get(() => strongReference.IsChecked);
            }
        }
    }
}