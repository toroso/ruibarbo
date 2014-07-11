﻿using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.core.Wpf
{
    public class WpfTooltip : WpfTooltipBase<System.Windows.Controls.ToolTip>, IRegisteredElement
    {
        public WpfTooltip(ISearchSourceElement searchParent, System.Windows.Controls.ToolTip frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}