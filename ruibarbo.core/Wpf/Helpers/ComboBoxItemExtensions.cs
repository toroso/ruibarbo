using ruibarbo.core.Common;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Base;

namespace ruibarbo.core.Wpf.Helpers
{
    public static class ComboBoxItemExtensions
    {
        public static string TextBlockText(this IComboBoxItem me)
        {
            return me.FindFirstChild<WpfTextBlock>().Text;
        }

        public static void OpenAndClick(this IComboBoxItem me)
        {
            var itemsContainer = me.FindFirstAncestor<WpfComboBox>();
            itemsContainer.Open();

            bool isVisible = Wait.Until(() => me.IsVisible);
            if (!isVisible)
            {
                throw RuibarboException.StateFailed(me, x => x.IsVisible);
            }

            me.Click();
        }
    }
}