﻿using tungsten.core.ElementFactory;
using tungsten.core.Search;
using tungsten.core.Wpf.Base;
using tungsten.core.Wpf.Factory;

namespace tungsten.sampletest.AutomationLayer
{
    public class Tab4Control : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement
    {
        public Tab4Control(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public Muppets4Expander Muppets4Expander
        {
            get { return this.FindFirstChild<Muppets4Expander>(By.Name("ExpMuppets4")); }
        }
    }
}