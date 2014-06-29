using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfContentPresenter : WpfContentPresenterBase<System.Windows.Controls.ContentPresenter>, IRegisteredElement
    {
        public WpfContentPresenter(ISearchSourceElement searchParent, System.Windows.Controls.ContentPresenter frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}