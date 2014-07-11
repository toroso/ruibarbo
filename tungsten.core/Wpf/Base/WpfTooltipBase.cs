using System.Collections.Generic;
using tungsten.core.Search;

namespace tungsten.core.Wpf.Base
{
    public class WpfTooltipBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ToolTip
    {
        public WpfTooltipBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override IEnumerable<object> NativeChildren
        {
            get
            {
                var root = Invoker.Get(this, frameworkElement => frameworkElement.Content);

                if (root != null)
                {
                    yield return root;
                }
            }
        }
    }
}