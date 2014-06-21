using System;
using System.Windows;
using tungsten.core.Elements;
using tungsten.core.Input;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.BaseElements
{
    public class WpfComboBoxItemBase<TNativeElement> : WpfContentControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ComboBoxItem
    {
        public WpfComboBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override FrameworkElement NativeParent
        {
            get { return Invoker.Get(this, System.Windows.Controls.ItemsControl.ItemsControlFromItemContainer); }
        }

        public override void Click()
        {
            this.BringIntoView();
            System.Threading.Thread.Sleep(20); // Takes a while for ComboBoxes to open and scroll... TODO: Configurable timespan.
            // Better TODO: Wait until it is in view. How?

            var bounds = this.BoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }
    }

    public static class WpfComboBoxItemBaseExtensions
    {
        public static void OpenAndClick<TNativeElement>(this WpfComboBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBoxItem
        {
            var itemsContainer = me.FindFirstAncestor<WpfComboBox>();
            itemsContainer.Open();

            bool isVisible = Wait.Until(() => me.IsVisible, TimeSpan.FromSeconds(5));
            if (!isVisible)
            {
                throw ManglaException.NotVisible(me);
            }

            me.Click();
        }

        public static bool IsSelected<TNativeElement>(this WpfComboBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBoxItem
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsSelected);
        }
    }
}