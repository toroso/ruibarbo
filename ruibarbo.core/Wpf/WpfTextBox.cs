using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Base;
using ruibarbo.core.Wpf.Factory;

namespace ruibarbo.core.Wpf
{
    public sealed class WpfTextBox : WpfTextBoxBase<System.Windows.Controls.TextBox>, IRegisteredElement
    {
        public WpfTextBox(ISearchSourceElement searchParent, System.Windows.Controls.TextBox textBox)
            : base(searchParent, textBox)
        {
        }
    }
}