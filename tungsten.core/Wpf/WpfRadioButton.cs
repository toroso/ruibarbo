namespace tungsten.core.Wpf.Base
{
    public class WpfRadioButton : WpfRadioButtonBase<System.Windows.Controls.RadioButton>, IRegisteredElement<System.Windows.Controls.RadioButton>
    {
        public WpfRadioButton(ISearchSourceElement searchParent, System.Windows.Controls.RadioButton frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}