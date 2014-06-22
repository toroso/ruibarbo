using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfCheckBox : WpfCheckBoxBase<System.Windows.Controls.CheckBox>, IRegisteredElement<System.Windows.Controls.CheckBox>
    {
        public WpfCheckBox(ISearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }
    }
}