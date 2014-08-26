using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfDataGrid : WpfDataGridBase<System.Windows.Controls.DataGrid>
    {
        public WpfDataGrid(ISearchSourceElement searchParent, System.Windows.Controls.DataGrid frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}