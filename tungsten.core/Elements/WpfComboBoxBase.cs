using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfComboBoxBase<TNativeElement> : WpfItemsControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ComboBox
    {
        public WpfComboBoxBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
            DisableTimeConsumingAnimations();
        }

        private void DisableTimeConsumingAnimations()
        {
            Invoker.Invoke(this, fe =>
                {
                    var popup = (Popup) fe.Template.FindName("PART_Popup", fe);
                    popup.PopupAnimation = PopupAnimation.None;
                });
        }

        // TODO: Override Children
    }

    public static class WpfComboBoxBaseExtensions
    {
        // TODO: Remove. Only support IsSelected on WpfItemBase.
        public static WpfComboBoxItem SelectedItem<TNativeElement>(this WpfComboBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            // TODO: What if selectedItem is null?
            var selectedItem = Invoker.Get(me, frameworkElement => (System.Windows.Controls.ComboBoxItem)frameworkElement.SelectedItem);
            return CreateWpfComboBoxItem(selectedItem, me).First();
        }

        public static bool IsDropDownOpen<TNativeElement>(this WpfComboBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsDropDownOpen);
        }

        // TODO: Would be a nice with a ChangeSelectedItemTo<TNativeElement, TNativeItem>(me, params By[] bys), although messy to use

        public static void ChangeSelectedItemTo<TNativeElement, TNativeItem>(this WpfComboBoxBase<TNativeElement> me, WpfComboBoxItemBase<TNativeItem> item)
            where TNativeElement : System.Windows.Controls.ComboBox
            where TNativeItem : System.Windows.Controls.ComboBoxItem
        {
            me.Click();
            bool isVisible = Wait.Until(item.IsVisible, TimeSpan.FromSeconds(5));
            if (!isVisible)
            {
                throw ManglaException.NotVisible(item);
            }

            item.Click();
        }

        // TODO: Return WpfComboBoxItemBase... in some way.
        private static IEnumerable<WpfComboBoxItem> CreateWpfComboBoxItem<TNativeItem, TNativeParent>(TNativeItem item, WpfComboBoxBase<TNativeParent> parent)
            where TNativeItem : System.Windows.Controls.ComboBoxItem
            where TNativeParent : System.Windows.Controls.ComboBox
        {
            return ElementFactory.ElementFactory.CreateWpfElements(parent, item).OfType<WpfComboBoxItem>();
        }
    }
}