using tungsten.core.Hardware;
using tungsten.core.Search;

namespace tungsten.core.Wpf.Base
{
    public class WpfExpanderBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.Expander
    {
        public WpfExpanderBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override void Click()
        {
            var header = Header;
            header.BringIntoView();
            Mouse.Click(header);
        }

        // TODO: Circular dependency
        public WpfFrameworkElement Header
        {
            get { return this.FindFirstChild<WpfFrameworkElement>(By.Name("HeaderSite")); }
        }

        public bool IsExpanded
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsExpanded); }
        }
    }
}