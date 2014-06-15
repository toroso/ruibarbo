namespace tungsten.core.Elements
{
    public class WpfCheckBox : WpfCheckBoxBase<System.Windows.Controls.CheckBox>, IRegisteredElement<System.Windows.Controls.CheckBox>
    {
        public WpfCheckBox(SearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }
    }
}