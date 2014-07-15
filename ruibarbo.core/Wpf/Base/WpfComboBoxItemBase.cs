using ruibarbo.core.Common;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfComboBoxItemBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>, IComboBoxItem
        where TNativeElement : System.Windows.Controls.ComboBoxItem
    {
        public WpfComboBoxItemBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }

        public override object NativeParent
        {
            get { return OnUiThread.Get(this, System.Windows.Controls.ItemsControl.ItemsControlFromItemContainer); }
        }
    }

    public interface IComboBoxItem : ISearchSourceElement
    {
    }

    public static class WpfComboBoxItemBaseExtensions
    {
        public static void OpenAndClick(this IComboBoxItem me)
        {
            // TODO: This creates a circular dependency.
            //  * Inject parent?
            var itemsContainer = me.FindFirstAncestor<WpfComboBox>();
            itemsContainer.Open();

            bool isVisible = Wait.Until(() => me.IsVisible);
            if (!isVisible)
            {
                throw ManglaException.StateFailed(me, x => x.IsVisible);
            }

            me.Click();
        }

        public static bool IsSelected<TNativeElement>(this WpfComboBoxItemBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.ComboBoxItem
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsSelected);
        }
    }
}