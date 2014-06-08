using System;
using System.Windows.Controls;

namespace tungsten.core.Elements
{
    public class WpfCheckBox : WpfElement<System.Windows.Controls.CheckBox>
    {
        public WpfCheckBox(SearchSourceElement searchParent, System.Windows.Controls.CheckBox checkBox)
            : base(searchParent, checkBox)
        {
        }

        public bool? IsChecked
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsChecked); }
        }
    }
}