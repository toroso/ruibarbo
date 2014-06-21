using System;
using System.Windows.Controls.Primitives;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.BaseElements
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

        public bool IsDropDownOpen
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsDropDownOpen); }
        }

        public void ChangeSelectedItemToFirst<TItem>(params By[] bys)
            where TItem : class, ISearchSourceElement
        {
            var item = FindFirstItem<TItem>(bys);
            // TODO: Use item.OpenAndClick(). How? Interface IComboBoxItem perhaps?
            this.ChangeSelectedItemTo(item);
        }

        // TODO: Override Children

        public void Open()
        {
            if (!IsDropDownOpen)
            {
                Click();
                bool isOpen = Wait.Until(() => IsDropDownOpen, TimeSpan.FromSeconds(5));
                if (!isOpen)
                {
                    throw ManglaException.NotOpen(this);
                }
            }
        }
    }

    public static class WpfComboBoxBaseExtensions
    {
        public static bool IsDropDownOpen<TNativeElement>(this WpfComboBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsDropDownOpen);
        }

        // TODO? Remove. Use WpfComboBoxItemBase.OpenAndClick()
        public static void ChangeSelectedItemTo<TNativeElement, TItem>(this WpfComboBoxBase<TNativeElement> me, TItem item)
            where TNativeElement : System.Windows.Controls.ComboBox
            where TItem : ISearchSourceElement
        {
            me.Click();
            bool isVisible = Wait.Until(() => item.IsVisible, TimeSpan.FromSeconds(5));
            if (!isVisible)
            {
                throw ManglaException.NotVisible(item);
            }

            item.Click();
        }
    }
}