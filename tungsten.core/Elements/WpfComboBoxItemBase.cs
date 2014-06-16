using tungsten.core.Input;

namespace tungsten.core.Elements
{
    public class WpfComboBoxItemBase<TNativeElement> : WpfContentControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ComboBoxItem
    {
        public WpfComboBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfComboBoxItemBaseExtensions
    {
        public static void Click<TFrameworkElement>(this WpfComboBoxItemBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.Controls.ComboBoxItem
        {
            me.BringIntoView();
            System.Threading.Thread.Sleep(20); // Takes a while for ComboBoxes to open and scroll... TODO: Configurable timespan.
            // Better TODO: Wait until it is in view. How?

            var bounds = me.BoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }
    }
}