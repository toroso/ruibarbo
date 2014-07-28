using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    [RegisteredElement]
    public sealed class WpfImage : WpfImageBase<System.Windows.Controls.Image>
    {
        public WpfImage(ISearchSourceElement searchParent, System.Windows.Controls.Image frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}