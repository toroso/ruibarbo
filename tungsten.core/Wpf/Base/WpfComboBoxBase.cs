using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using tungsten.core.Search;
using tungsten.core.Wpf.Invoker;

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
            OnUiThread.Invoke(this, fe =>
                {
                    var popup = (Popup) fe.Template.FindName("PART_Popup", fe);
                    popup.PopupAnimation = PopupAnimation.None;
                });
        }

        public bool IsDropDownOpen
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsDropDownOpen); }
        }

        public override IEnumerable<TWpfItem> AllItems<TWpfItem>()
        {
            return WithOpenComboBoxDo(() => base.AllItems<TWpfItem>());
        }

        public TWpfItem SelectedItem<TWpfItem>()
            where TWpfItem : class, IComboBoxItem
        {
            return WithOpenComboBoxDo(() =>
                {
                    var nativeElement = OnUiThread.Get(this, frameworkElement =>
                        {
                            var selectedItem = frameworkElement.SelectedItem;
                            return selectedItem is System.Windows.FrameworkElement
                                ? selectedItem
                                : frameworkElement.ItemContainerGenerator.ContainerFromItem(selectedItem);
                        });
                    return nativeElement != null
                        ? ElementFactory.ElementFactory.CreateElements(this, nativeElement)
                            .OfType<TWpfItem>()
                            .First(item => item.GetType() == typeof(TWpfItem))
                        : null;
                });
        }

        private TRet WithOpenComboBoxDo<TRet>(Func<TRet> func)
        {
            var wasOpen = IsDropDownOpen;
            Open();

            try
            {
                return func();
            }
            finally
            {
                if (!wasOpen)
                {
                    Close();
                }
            }
        }

        public void OpenAndClickFirst<TItem>()
            where TItem : class, IComboBoxItem
        {
            OpenAndClickFirst<TItem>(By.Empty);
        }

        public void OpenAndClickFirst<TItem>(params Func<IByBuilder<TItem>, By>[] byBuilders)
            where TItem : class, IComboBoxItem
        {
            OpenAndClickFirst<TItem>(byBuilders.Build());
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
                bool isOpen = Wait.Until(() => IsDropDownOpen);
                if (!isOpen)
                {
                    throw ManglaException.StateFailed(this, x => x.IsDropDownOpen);
                }
            }
        }

        public void Close()
        {
            if (IsDropDownOpen)
            {
                Click();
                bool isClosed = Wait.Until(() => !IsDropDownOpen);
                if (!isClosed)
                {
                    throw ManglaException.StateFailed(this, x => !x.IsDropDownOpen);
                }
            }
        }
    }

    public static class WpfComboBoxBaseExtensions
    {
        public static bool IsDropDownOpen<TNativeElement>(this WpfComboBoxBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBox
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsDropDownOpen);
        }
    }
}