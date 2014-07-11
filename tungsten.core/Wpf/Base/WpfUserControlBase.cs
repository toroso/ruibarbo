﻿using tungsten.core.Search;

namespace tungsten.core.Wpf.Base
{
    public class WpfUserControlBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.UserControl
    {
        public WpfUserControlBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}