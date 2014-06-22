using tungsten.core.Wpf.Base;

namespace tungsten.core.Wpf
{
    public class WpfTextBox : WpfTextBoxBase<System.Windows.Controls.TextBox>, IRegisteredElement<System.Windows.Controls.TextBox>
    {
        public WpfTextBox(ISearchSourceElement searchParent, System.Windows.Controls.TextBox textBox)
            : base(searchParent, textBox)
        {
        }
    }
}