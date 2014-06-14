using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfComboBox : WpfFrameworkElementBase<System.Windows.Controls.ComboBox>, IRegisteredElement<System.Windows.Controls.ComboBox>
    {
        public WpfComboBox(SearchSourceElement searchParent, System.Windows.Controls.ComboBox frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfComboBoxExtensions
    {
        public static IEnumerable<WpfComboBoxItem> Items(this WpfComboBox me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<System.Windows.Controls.ComboBoxItem>()
                .SelectMany(item => CreateWpfComboBoxItem(item, me))
                .ToArray();
        }

        public static WpfComboBoxItem SelectedItem(this WpfComboBox me)
        {
            // TODO: What if selectedItem is null?
            var selectedItem = Invoker.Get(me, frameworkElement => (System.Windows.Controls.ComboBoxItem)frameworkElement.SelectedItem);
            return CreateWpfComboBoxItem(selectedItem, me).First();
        }

        public static bool IsDropDownOpen(this WpfComboBox me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsDropDownOpen);
        }

        public static void ChangeSelectedItemTo(this WpfComboBox me, string itemAsString)
        {
            bool found = Wait.Until(() => me.Items().FirstOrDefault(i => i.Content().Equals(itemAsString)) != null, TimeSpan.FromSeconds(5));
            if (!found)
            {
                var bys = new[] { By.Content(itemAsString) };
                string foundAsString = me.Items().Select(i => string.Format("    '{0}'", i.Content())).Join("\n");
                throw ManglaException.FindFailed("item", me, bys, foundAsString);
            }
            var wrappedItem = me.Items().First(i => i.Content().Equals(itemAsString));
            me.ChangeSelectedItemTo(wrappedItem);
        }

        public static void ChangeSelectedItemTo(this WpfComboBox me, WpfComboBoxItem item)
        {
            me.Click();
            bool isVisible = Wait.Until(item.IsVisible, TimeSpan.FromSeconds(5));
            if (!isVisible)
            {
                throw ManglaException.NotVisible(item);
            }

            item.BringIntoView();
            System.Threading.Thread.Sleep(200); // Takes a while to open and scroll... TODO: Configurable timespan.
            item.Click();
        }

        private static IEnumerable<WpfComboBoxItem> CreateWpfComboBoxItem(System.Windows.Controls.ComboBoxItem item, WpfComboBox parent)
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item).OfType<WpfComboBoxItem>();
        }
    }
}