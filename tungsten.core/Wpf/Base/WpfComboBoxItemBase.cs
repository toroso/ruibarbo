using System;
using tungsten.core.Hardware;
using tungsten.core.Search;

namespace tungsten.core.Wpf.Base
{
    public class WpfComboBoxItemBase<TNativeElement> : WpfContentControlBase<TNativeElement>, IComboBoxItem
        where TNativeElement : System.Windows.Controls.ComboBoxItem
    {
        public WpfComboBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override object NativeParent
        {
            get { return Invoker.Get(this, System.Windows.Controls.ItemsControl.ItemsControlFromItemContainer); }
        }

        public override void Click()
        {
            this.BringIntoView();
            System.Threading.Thread.Sleep(20); // Takes a while for ComboBoxes to open and scroll... TODO: Configurable timespan.
            // Better TODO: Wait until it is in view. How?
            Mouse.Click(this);
        }
    }

    public interface IComboBoxItem : ISearchSourceElement
    {
    }

    public static class WpfComboBoxItemBaseExtensions
    {
        public static void OpenAndClick(this IComboBoxItem me)
        {
            var itemsContainer = me.FindFirstAncestor<WpfComboBox>();
            itemsContainer.Open();

            bool isVisible = Wait.Until(() => me.IsVisible, TimeSpan.FromSeconds(5));
            if (!isVisible)
            {
                throw ManglaException.StateFailed(me, x => x.IsVisible);
            }

            me.Click();
        }

        public static bool IsSelected<TNativeElement>(this WpfComboBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBoxItem
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsSelected);
        }

        public static string TextBlockText(this IComboBoxItem me)
        {
            return me.FindFirstChild<WpfTextBlock>().Text();
        }
    }
}