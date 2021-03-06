using ruibarbo.core.ElementFactory;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfTabControlBase<TNativeElement> : WpfItemsControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TabControl
    {
        public WpfTabControlBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}