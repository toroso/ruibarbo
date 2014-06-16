using System;
using System.Collections.Generic;
using System.Windows;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public class WpfTabItemBase<TNativeElement> : WpfContentControlBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TabItem
    {
        public WpfTabItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override IEnumerable<FrameworkElement> NativeChildren
        {
            get
            {
                // A WpfTabItem's frameworkElement (TabItem) is the header only. The content part belongs to TabControl.
                // But the TabControl's content host only contains the elements of the selected tab item.
                if (this.IsSelected())
                {
                    var owner = this.FindFirstAncestor<WpfTabControl>();
                    var contentPanel = owner.FindFirstChild<WpfFrameworkElement>(By.Name("PART_SelectedContentHost"));
                    yield return Invoker.Get(contentPanel, frameworkElement => frameworkElement);
                }

                foreach (var headerChild in base.NativeChildren)
                {
                    yield return headerChild;
                }
            }
        }
    }

    public static class WpfTabItemBaseExtensions
    {
        // TODO: Move to WpfHeaderedContentControl?
        public static object Header<TNativeElement>(this WpfTabItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TabItem
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Header);
        }

        public static bool IsSelected<TNativeElement>(this WpfTabItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TabItem
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsSelected);
        }

        public static void Click<TNativeElement>(this WpfTabItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TabItem
        {
            // Complicated because Click() is an extension method. Had Click() been a member I could've used normal overload.
            var baseType = (WpfFrameworkElementBase<TNativeElement>)me;
            baseType.Click();

            // It takes a while for a TabItem to be selected
            Wait.Until(me.IsSelected, TimeSpan.FromSeconds(5));
        }
    }
}