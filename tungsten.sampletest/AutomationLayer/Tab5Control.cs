using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Wpf.Base;

namespace tungsten.sampletest.AutomationLayer
{
    public class Tab5Control : WpfTabItemBase<System.Windows.Controls.TabItem>, IRegisteredElement
    {
        public Tab5Control(ISearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public MuppetsExpander MuppetsExpander
        {
            get { return this.FindFirstChild<MuppetsExpander>(By.Name("ExpMuppets")); }
        }
    }
}