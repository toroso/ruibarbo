﻿using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfFrameworkElement
        : WpfFrameworkElementBase<System.Windows.FrameworkElement>, IRegisteredElement<System.Windows.FrameworkElement>
    {
        public WpfFrameworkElement(ISearchSourceElement searchParent, System.Windows.FrameworkElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}