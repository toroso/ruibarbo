using tungsten.core.Elements;
using tungsten.core.Search;

namespace tungsten.sampletest.AutomationLayer
{
    public class StuffControl : WpfUserControlBase<System.Windows.Controls.UserControl>, IRegisteredElement<System.Windows.Controls.UserControl>
    {
        public StuffControl(SearchSourceElement searchParent, System.Windows.Controls.UserControl frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public WpfComboBox ShowErrorComboBox
        {
            get { return this.FindFirstChild<WpfComboBox>(By.Name("CmbShowError")); }
        }

        public WpfTextBlock ErrorTextBlock
        {
            get { return this.FindFirstChild<WpfTextBlock>(By.Name("TxbError")); }
        }

        public WpfLabel InputLabel
        {
            get { return this.FindFirstChild<WpfLabel>(By.Name("LblInput")); }
        }
    }
}