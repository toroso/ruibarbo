using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Helpers;

namespace ruibarbo.core.Wpf.Search
{
    public static class ComboBoxByExtensions
    {
        // TODO: Merge with Helpers?
        public static By FirstTextBlockText(this IByBuilder<WpfComboBoxItem> me, string text)
        {
            return me.ByExpression(x => x.TextBlockText(), text);
        }
    }
}