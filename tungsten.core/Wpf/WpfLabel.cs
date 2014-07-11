using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfLabel : WpfLabelBase<System.Windows.Controls.Label>, IRegisteredElement
    {
        public WpfLabel(ISearchSourceElement searchParent, System.Windows.Controls.Label frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}