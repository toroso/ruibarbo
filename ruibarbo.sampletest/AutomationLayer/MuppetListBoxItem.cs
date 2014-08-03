using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    [RegisteredElement]
    public class MuppetListBoxItem : WpfListBoxItemBase<System.Windows.Controls.ListBoxItem>
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
            return me.ByExpression(x => x.MuppetTextBlock.Text, text);
        }
    }
}