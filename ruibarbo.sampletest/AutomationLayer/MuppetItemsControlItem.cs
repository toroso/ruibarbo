using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    public class MuppetItemsControlItem : WpfContentPresenterBase<System.Windows.Controls.ContentPresenter>, IRegisteredElement
    {
        public MuppetItemsControlItem(ISearchSourceElement searchParent, System.Windows.Controls.ContentPresenter frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfTextBox MuppetTextBox
        {
            get { return this.FindFirstChild<WpfTextBox>(By.Name("TxtMuppet")); }
        }
    }
}