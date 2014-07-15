using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfRadioButton : WpfRadioButtonBase<System.Windows.Controls.RadioButton>
    {
        public WpfRadioButton(ISearchSourceElement searchParent, System.Windows.Controls.RadioButton frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}