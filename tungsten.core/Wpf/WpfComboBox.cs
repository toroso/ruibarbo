﻿using tungsten.core.ElementFactory;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public sealed class WpfComboBox : WpfComboBoxBase<System.Windows.Controls.ComboBox>, IRegisteredElement
    {
        public WpfComboBox(ISearchSourceElement searchParent, System.Windows.Controls.ComboBox frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}