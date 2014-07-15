using ruibarbo.core.Search;

namespace ruibarbo.core.Wpf.Helpers
{
    public static class ComboBoxByExtensions
    {
        public static By FirstTextBlockText(this IByBuilder<WpfComboBoxItem> me, string text)
        {
            return me.ByExpression(x => x.TextBlockText(), text);
        }
    }
}