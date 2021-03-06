using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using ruibarbo.core.Common;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfComboBoxBase<TNativeElement> : WpfItemsControlBase<TNativeElement>, IComboBox
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

        public void Open()
        {
            if (!IsDropDownOpen)
            {
                Click();
                bool isOpen = Wait.Until(() => IsDropDownOpen);
                if (!isOpen)
                {
                    throw RuibarboException.StateFailed(this, x => x.IsDropDownOpen);
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
                    throw RuibarboException.StateFailed(this, x => !x.IsDropDownOpen);
                }
            }
        }
    }
}