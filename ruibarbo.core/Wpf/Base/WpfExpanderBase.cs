using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
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
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsExpanded); }
        }
    }
}