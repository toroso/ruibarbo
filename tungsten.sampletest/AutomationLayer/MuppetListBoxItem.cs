using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;

namespace tungsten.sampletest.AutomationLayer
{
    public class MuppetListBoxItem : WpfListBoxItemBase<System.Windows.Controls.ListBoxItem>, IRegisteredElement
    {
        public MuppetListBoxItem(ISearchSourceElement searchParent, System.Windows.Controls.ListBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfTextBlock MuppetTextBlock
        {
            get { return this.FindFirstChild<WpfTextBlock>(By.Name("TxbMuppet")); }
        }
    }
}