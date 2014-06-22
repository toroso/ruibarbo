using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfLabel : WpfLabelBase<System.Windows.Controls.Label>, IRegisteredElement<System.Windows.Controls.Label>
    {
        public WpfLabel(ISearchSourceElement searchParent, System.Windows.Controls.Label frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}