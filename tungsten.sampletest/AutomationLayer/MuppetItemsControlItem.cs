using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;

namespace tungsten.sampletest.AutomationLayer
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