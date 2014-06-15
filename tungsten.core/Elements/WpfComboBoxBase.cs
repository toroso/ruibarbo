using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfComboBoxBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ComboBox
    {
        public WpfComboBoxBase(SearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        // TODO: Override Children
    }

    public static class WpfComboBoxBaseExtensions
    {
        public static IEnumerable<WpfComboBoxItem> Items<TNativeElement>(this WpfComboBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Items)
                .Cast<System.Windows.Controls.ComboBoxItem>()
                .SelectMany(item => CreateWpfComboBoxItem(item, me))
                .ToArray();
        }

        public static WpfComboBoxItem SelectedItem<TNativeElement>(this WpfComboBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            // TODO: What if selectedItem is null?
            var selectedItem = Invoker.Get(me, frameworkElement => (System.Windows.Controls.ComboBoxItem)frameworkElement.SelectedItem);
            return CreateWpfComboBoxItem(selectedItem, me).First();
        }

        public static bool IsDropDownOpen<TNativeElement>(this WpfComboBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsDropDownOpen);
        }

        public static void ChangeSelectedItemTo<TNativeElement>(this WpfComboBoxBase<TNativeElement> me, string itemAsString)
            where TNativeElement : System.Windows.Controls.ComboBox
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

        public static void ChangeSelectedItemTo<TNativeElement>(this WpfComboBoxBase<TNativeElement> me, WpfComboBoxItem item)
            where TNativeElement : System.Windows.Controls.ComboBox
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

        // TODO: Return WpfComboBoxItemBase... in some way.
        private static IEnumerable<WpfComboBoxItem> CreateWpfComboBoxItem<TNativeItem, TNativeParent>(TNativeItem item, WpfComboBoxBase<TNativeParent> parent)
            where TNativeItem : System.Windows.Controls.ComboBoxItem
            where TNativeParent : System.Windows.Controls.ComboBox
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item).OfType<WpfComboBoxItem>();
        }
    }
}