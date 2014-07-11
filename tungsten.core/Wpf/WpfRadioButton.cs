using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfRadioButton : WpfRadioButtonBase<System.Windows.Controls.RadioButton>, IRegisteredElement
    {
        public WpfRadioButton(ISearchSourceElement searchParent, System.Windows.Controls.RadioButton frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}