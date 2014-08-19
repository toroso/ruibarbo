using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfPopup : WpfPopupBase<System.Windows.Controls.Primitives.Popup>
    {
        public WpfPopup(ISearchSourceElement searchParent, System.Windows.Controls.Primitives.Popup frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}