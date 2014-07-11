using tungsten.core.Search;
using tungsten.core.Wpf.Helpers;

namespace tungsten.core.Wpf.Search
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