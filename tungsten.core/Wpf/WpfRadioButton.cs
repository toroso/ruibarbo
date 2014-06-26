namespace tungsten.core.Wpf.Base
{
    public class WpfRadioButton : WpfRadioButtonBase<System.Windows.Controls.RadioButton>, IRegisteredElement
    {
        public WpfRadioButton(ISearchSourceElement searchParent, System.Windows.Controls.RadioButton frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}