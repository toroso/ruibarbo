using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public sealed class WpfCheckBox : WpfCheckBoxBase<System.Windows.Controls.CheckBox>, IRegisteredElement
    {
        public WpfCheckBox(ISearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }
    }
}