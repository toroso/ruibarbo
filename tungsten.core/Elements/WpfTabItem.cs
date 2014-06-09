using System.Collections.Generic;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public class WpfTabItem : WpfElement<System.Windows.Controls.TabItem>
    {
        public WpfTabItem(SearchSourceElement searchParent, System.Windows.Controls.TabItem frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override IEnumerable<UntypedWpfElement> Children
        {
            get
            {
                // A WpfTabItem's frameworkElement (TabItem) is the header only. The content part belongs to TabControl.
                // But the TabControl's content host only contains the elements of the selected tab item.
                if (this.IsSelected())
                {
                    var owner = this.FindFirstAncestor<WpfTabControl>();
                    var contentPanel = owner.FindFirstChild<WpfFrameworkElement>(By.Name("PART_SelectedContentHost"));
                    yield return contentPanel;
                }

                foreach (var headerChild in base.Children)
                {
                    yield return headerChild;
                }
            }
        }
    }

    public static class WpfTabItemExtensions
    {
        public static object Header(this WpfTabItem me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Header);
        }

        public static bool IsSelected(this WpfTabItem me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsSelected);
        }
    }
}