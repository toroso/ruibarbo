using ruibarbo.core.ElementFactory;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfDataGridBase<TNativeElement> : WpfItemsControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.DataGrid
    {
        public WpfDataGridBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}