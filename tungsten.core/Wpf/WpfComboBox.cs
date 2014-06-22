﻿using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfComboBox : WpfComboBoxBase<System.Windows.Controls.ComboBox>, IRegisteredElement<System.Windows.Controls.ComboBox>
    {
        public WpfComboBox(ISearchSourceElement searchParent, System.Windows.Controls.ComboBox frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}