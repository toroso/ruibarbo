using tungsten.core.Search;
using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf.Helpers
{
    public static class ComboBoxItemExtensions
    {
        public static string TextBlockText(this IComboBoxItem me)
        {
            return me.FindFirstChild<WpfTextBlock>().Text();
        }
    }
}