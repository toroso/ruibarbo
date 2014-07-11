using tungsten.core.ElementFactory;

namespace tungsten.core.Wpf.Base
{
    public class WpfRadioButtonBase<TNativeElement> : WpfButtonBase<TNativeElement> // Actually WpfToggleButtonBase
        where TNativeElement : System.Windows.Controls.RadioButton
    {
        public WpfRadioButtonBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}