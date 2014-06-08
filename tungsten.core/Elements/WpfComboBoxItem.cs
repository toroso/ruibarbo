using System;
using System.Windows.Controls;

namespace tungsten.core.Elements
{
    public class WpfComboBoxItem : WpfElement<System.Windows.Controls.ComboBoxItem>
    {
        public WpfComboBoxItem(SearchSourceElement searchParent, System.Windows.Controls.ComboBoxItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public object Content
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Content); }
        }

        public void BringIntoView()
        {
            Invoker.Invoke(this, frameworkElement => frameworkElement.BringIntoView());
        }
    }
}