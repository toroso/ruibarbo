using System;
using System.Linq;
using tungsten.core.Search;
using tungsten.core.Wpf.Invoker;

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
            OnUiThread.Invoke(this, fe => fe.SetValue(System.Windows.Controls.VirtualizingPanel.IsVirtualizingProperty, false));
        }

        public TWpfItem SelectedItem<TWpfItem>()
            where TWpfItem : class, ISearchSourceElement
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
        }

        public void ClickFirst<TItem>()
            where TItem : class, ISearchSourceElement
        {
            ClickFirst<TItem>(By.Empty);
        }

        public void ClickFirst<TItem>(params Func<IByBuilder<TItem>, By>[] byBuilders)
            where TItem : class, ISearchSourceElement
        {
            ClickFirst<TItem>(byBuilders.Build());
        }

        public void ClickFirst<TItem>(params By[] bys)
            where TItem : class, ISearchSourceElement
        {
            var item = FindFirstItem<TItem>(bys);
            item.Click();
        }
    }

    public static class WpfListBoxBaseExtensions
    {
    }
}