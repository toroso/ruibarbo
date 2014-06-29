namespace tungsten.core.Wpf.Base
{
    public class WpfTooltipBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ToolTip
    {
        public WpfTooltipBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}