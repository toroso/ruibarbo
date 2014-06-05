using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfComboBox : WpfElement<System.Windows.Controls.ComboBox>
    {
        public WpfComboBox(SearchSourceElement parent, System.Windows.Controls.ComboBox frameworkElement)
            : base(parent, frameworkElement)
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
                // TODO: Error message, exception type
                throw new Exception(string.Format("Not found: '{0}'!", itemAsString));
            }
            var wrappedItem = Items.First(i => i.Content.Equals(itemAsString));
            ChangeSelectedItemTo(wrappedItem);
        }

        private void ChangeSelectedItemTo(WpfComboBoxItem item)
        {
            Click();
            bool isVisible = Wait.Until(() => item.IsVisible, TimeSpan.FromSeconds(5));
            if (!isVisible)
            {
                // TODO: Error message, exception type
                throw new Exception("Not visible");
            }
            System.Threading.Thread.Sleep(200); // Takes a while even though it's visible... TODO: Configurable timespan.
            item.Click();
        }
    }
}