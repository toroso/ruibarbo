using System;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using tungsten.core.Search;

namespace tungsten.core.Wpf.Base
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

        public override IEnumerable<TWpfItem> AllItems<TWpfItem>()
        {
            var wasOpen = IsDropDownOpen;
            Open();
            var x = base.AllItems<TWpfItem>();
            if (!wasOpen)
            {
                Close();
            }
            return x;
        }

        public void OpenAndClickFirst<TItem>(params By[] bys)
            where TItem : class, IComboBoxItem
        {
            var item = FindFirstItem<TItem>(bys);
            item.OpenAndClick();
        }

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

        public void Close()
        {
            if (IsDropDownOpen)
            {
                Click();
                bool isClosed = Wait.Until(() => !IsDropDownOpen, TimeSpan.FromSeconds(5));
                if (!isClosed)
                {
                    // TODO: Make some kind of state failed exception instead.
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
    }
}