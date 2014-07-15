using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfListBox : WpfListBoxBase<System.Windows.Controls.ListBox>
    {
        public WpfListBox(ISearchSourceElement searchParent, System.Windows.Controls.ListBox frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}