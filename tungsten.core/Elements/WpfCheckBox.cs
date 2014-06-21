using tungsten.core.BaseElements;

namespace tungsten.core.Elements
{
    public class WpfCheckBox : WpfCheckBoxBase<System.Windows.Controls.CheckBox>, IRegisteredElement<System.Windows.Controls.CheckBox>
    {
        public WpfCheckBox(ISearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }
    }
}