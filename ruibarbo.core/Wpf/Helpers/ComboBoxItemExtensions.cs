using System;

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

            // Don't know why I sometimes need this. IsClickable reports true, but still an item in the ComboBox is not clickable.
            if (Configuration.DelayWhenOpeningComboBox > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(Configuration.DelayWhenOpeningComboBox);
            }

            bool isVisible = Wait.Until(() => me.IsVisible);
            if (!isVisible)
            {
                throw RuibarboException.StateFailed(me, x => x.IsVisible);
            }

            me.Click();
        }
    }
}