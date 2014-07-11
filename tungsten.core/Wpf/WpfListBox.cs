using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfListBox : WpfListBoxBase<System.Windows.Controls.ListBox>, IRegisteredElement
    {
        public WpfListBox(ISearchSourceElement searchParent, System.Windows.Controls.ListBox frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}