using System.Collections.Generic;
using System.Linq;

namespace tungsten.core.Elements
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