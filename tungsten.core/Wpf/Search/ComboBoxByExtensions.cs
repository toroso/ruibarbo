using tungsten.core.Search;
using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf.Search
{
    public static class ComboBoxByExtensions
    {
        public static By FirstTextBlockText(this IByBuilder<WpfComboBoxItem> me, string text)
        {
            return me.ByExpression(x => x.TextBlockText(), text);
        }
    }
}