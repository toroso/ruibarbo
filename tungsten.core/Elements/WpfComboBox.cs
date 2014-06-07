using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfComboBox : WpfElement<System.Windows.Controls.ComboBox>
    {
        public WpfComboBox(SearchSourceElement searchParent, System.Windows.Controls.ComboBox frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public IEnumerable<WpfComboBoxItem> Items
        {
            get
            {
                return Get(frameworkElement => frameworkElement.Items
                    .Cast<System.Windows.Controls.ComboBoxItem>()
                    .Select(AsWpfComboBoxItem)
                    .ToArray());
            }
        }

        public WpfComboBoxItem SelectedItem
        {
            get { return Get(frameworkElement => AsWpfComboBoxItem(frameworkElement.SelectedItem)); }
        }

        public bool IsDropDownOpen
        {
            get { return Get(frameworkElement => frameworkElement.IsDropDownOpen); }
        }

        private WpfComboBoxItem AsWpfComboBoxItem(object item)
        {
            return new WpfComboBoxItem(this, (System.Windows.Controls.ComboBoxItem)item);
        }

        public void ChangeSelectedItemTo(string itemAsString)
        {
            bool found = Wait.Until(() => Items.FirstOrDefault(i => i.Content.Equals(itemAsString)) != null, TimeSpan.FromSeconds(5));
            if (!found)
            {
                var bys = new[] { By.Content(itemAsString) };
                string foundAsString = Items.Select(i => string.Format("    '{0}'", i.Content)).Join("\n");
                throw ManglaException.FindFailed("item", this, bys, foundAsString);
            }
            var wrappedItem = Items.First(i => i.Content.Equals(itemAsString));
            ChangeSelectedItemTo(wrappedItem);
        }

        public void ChangeSelectedItemTo(WpfComboBoxItem item)
        {
            Click();
            bool isVisible = Wait.Until(() => item.IsVisible, TimeSpan.FromSeconds(5));
            if (!isVisible)
            {
                throw ManglaException.NotVisible(item, this);
            }

            item.BringIntoView();
            System.Threading.Thread.Sleep(200); // Takes a while to open and scroll... TODO: Configurable timespan.
            item.Click();
        }
    }
}