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

            // TODO: Don't know why I need this. It is IsInView in BringIntoView that is not working?
            // Probably that GetParent() does not return the container I want.
            System.Threading.Thread.Sleep(Configuration.DelayWhenOpeningComboBox);
            me.Click();
        }
    }
}