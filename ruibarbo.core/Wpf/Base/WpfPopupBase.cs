using System.Collections.Generic;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfPopupBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.Primitives.Popup
    {
        public WpfPopupBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override IEnumerable<object> NativeChildren
        {
            get
            {
                var root = OnUiThread.Get(this, frameworkElement => frameworkElement.Child);

                if (root != null)
                {
                    yield return root;
                }
            }
        }
    }
}