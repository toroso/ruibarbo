﻿using System.Linq;

namespace tungsten.core.Wpf.Base
{
    public class WpfListBoxBase<TNativeElement> : WpfItemsControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.ListBox
    {
        public WpfListBoxBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
            DisableItemCreationVirtualization();
        }

        private void DisableItemCreationVirtualization()
        {
            // With virtualization turned on, ListBoxItems are not created until the are visible (scrolled into view). This
            // causes many problems when testing, so here it is swtiched off.
            Invoker.Invoke(this, fe => fe.SetValue(System.Windows.Controls.VirtualizingPanel.IsVirtualizingProperty, false));
        }

        public TWpfItem SelectedItem<TWpfItem>()
            where TWpfItem : class, ISearchSourceElement
        {
            var nativeElement = Invoker.Get(this, frameworkElement =>
                {
                    var selectedItem = frameworkElement.SelectedItem;
                    return selectedItem is System.Windows.FrameworkElement
                        ? selectedItem
                        : frameworkElement.ItemContainerGenerator.ContainerFromItem(selectedItem);
                });
            return nativeElement != null
                ? ElementFactory.ElementFactory.CreateElements(this, nativeElement).OfType<TWpfItem>().First()
                : null;
        }
    }
}