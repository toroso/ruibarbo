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

        public virtual TElement ExpandButton<TElement>()
            where TElement : class, ISearchSourceElement
        {
            return this.FindFirstChild<TElement>(By.Name("HeaderSite"));
        }

        public bool IsExpanded
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsExpanded); }
        }
    }
}