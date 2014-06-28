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

        public WpfTextBox MuppetTextBox
        {
            get { return this.FindFirstChild<WpfTextBox>(By.Name("TxtMuppet")); }
        }
    }
}