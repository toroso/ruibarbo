using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    public class StuffControl : WpfUserControlBase<System.Windows.Controls.UserControl>, IRegisteredElement
    {
        public StuffControl(ISearchSourceElement searchParent, System.Windows.Controls.UserControl frameworkElement)
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

        public WpfTextBox InputTextBox
        {
            get { return this.FindFirstChild<WpfTextBox>(By.Name("TxtInput")); }
        }

        public WpfButton SubmitButton
        {
            get { return this.FindFirstChild<WpfButton>(By.Name("BtnSubmit")); }
        }

        public WpfRadioButton DisabledSubmitRadioButton
        {
            get { return this.FindFirstChild<WpfRadioButton>(By.Name("Option1")); }
        }

        public WpfRadioButton EnabledSubmitRadioButton
        {
            get { return this.FindFirstChild<WpfRadioButton>(By.Name("Option2")); }
        }
    }
}