namespace tungsten.core.Elements
{
    public class WpfTextBox : WpfTextBoxBase<System.Windows.Controls.TextBox>, IRegisteredElement<System.Windows.Controls.TextBox>
    {
        public WpfTextBox(SearchSourceElement searchParent, System.Windows.Controls.TextBox textBox)
            : base(searchParent, textBox)
        {
        }
    }
}