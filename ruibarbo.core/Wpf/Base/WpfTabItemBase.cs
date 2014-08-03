using System.Collections.Generic;
using ruibarbo.core.Common;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfTabItemBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TabItem
    {
        public WpfTabItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        // TODO: Move to WpfHeaderedContentControl?
        public object Header
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.Header); }
        }

        public bool IsSelected
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsSelected); }
        }

        public override IEnumerable<object> NativeChildren
        {
            get
            {
                // A WpfTabItem's frameworkElement (TabItem) is the header only. The content part belongs to TabControl.
                // But the TabControl's content host only contains the elements of the selected tab item.
                if (IsSelected)
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
            bool isSelected = Wait.Until(() => IsSelected);
            if (!isSelected)
            {
                throw RuibarboException.StateFailed(this, x => x.IsSelected);
            }
        }
    }
}