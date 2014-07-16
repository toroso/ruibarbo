using System;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Base;

namespace ruibarbo.core.Wpf.Helpers
{
    public static class ComboBoxExtensions
    {
        public static void OpenAndClickFirst<TItem>(this IComboBox me)
            where TItem : class, IComboBoxItem
        {
            OpenAndClickFirst<TItem>(me, By.Empty);
        }

        public static void OpenAndClickFirst<TItem>(this IComboBox me, params Func<IByBuilder<TItem>, By>[] byBuilders)
            where TItem : class, IComboBoxItem
        {
            OpenAndClickFirst<TItem>(me, byBuilders.Build());
        }

        public static void OpenAndClickFirst<TItem>(this IComboBox me, params By[] bys)
            where TItem : class, IComboBoxItem
        {
            var item = me.FindFirstItem<TItem>(bys);
            item.OpenAndClick();
        }
    }
}