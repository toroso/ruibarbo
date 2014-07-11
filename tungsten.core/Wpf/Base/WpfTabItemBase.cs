using System.Collections.Generic;
using tungsten.core.Common;
using tungsten.core.ElementFactory;
using tungsten.core.Search;
using tungsten.core.Wpf.Invoker;

namespace tungsten.core.Wpf.Base
{
    public class WpfTabItemBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TabItem
    {
        public WpfTabItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override IEnumerable<object> NativeChildren
        {
            get
            {
                // A WpfTabItem's frameworkElement (TabItem) is the header only. The content part belongs to TabControl.
                // But the TabControl's content host only contains the elements of the selected tab item.
                if (this.IsSelected())
                {
                    // TODO: This creates a circular dependency.
                    //  * Inject parent TabControl?
                    //  * Work directly with VisualTreeHelper and FrameworkELements?
                    var owner = this.FindFirstAncestor<WpfTabControl>();
                    var contentPanel = owner.FindFirstChild<WpfFrameworkElement>(By.Name("PART_SelectedContentHost"));
                    yield return OnUiThread.Get(contentPanel, frameworkElement => frameworkElement);
                }

                foreach (var headerChild in base.NativeChildren)
                {
                    yield return headerChild;
                }
            }
        }

        public override void Click()
        {
            base.Click();
            bool isSelected = Wait.Until(this.IsSelected);
            if (!isSelected)
            {
                throw ManglaException.StateFailed(this, x => x.IsSelected());
            }
        }
    }

    public static class WpfTabItemBaseExtensions
    {
        // TODO: Move to WpfHeaderedContentControl?
        public static object Header<TNativeElement>(this WpfTabItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TabItem
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.Header);
        }

        public static bool IsSelected<TNativeElement>(this WpfTabItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TabItem
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsSelected);
        }
    }
}