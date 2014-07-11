using tungsten.core.ElementFactory;
using tungsten.core.Search;
using tungsten.core.Wpf;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

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

    public static class MuppetListBoxByExtensions
    {
        public static By Muppet(this IByBuilder<MuppetListBoxItem> me, string text)
        {
            return me.ByExpression(x => x.MuppetTextBlock.Text(), text);
        }
    }
}