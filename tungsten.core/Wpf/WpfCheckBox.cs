using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfCheckBox : WpfCheckBoxBase<System.Windows.Controls.CheckBox>, IRegisteredElement
    {
        public WpfCheckBox(ISearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }
    }
}