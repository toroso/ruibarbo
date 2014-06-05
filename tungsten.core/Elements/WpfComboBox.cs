using System.Collections.Generic;
using System.Linq;

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

        private WpfComboBoxItem AsWpfComboBoxItem(object item)
        {
            return new WpfComboBoxItem(this, (System.Windows.Controls.ComboBoxItem)item);
        }
    }
}