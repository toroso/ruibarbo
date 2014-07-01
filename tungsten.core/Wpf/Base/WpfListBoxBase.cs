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
    }
}