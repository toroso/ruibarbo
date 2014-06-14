namespace tungsten.core.Elements
{
    public class WpfLabel : WpfElement<System.Windows.Controls.Label>, IRegisteredElement<System.Windows.Controls.Label>
    {
        public WpfLabel(SearchSourceElement searchParent, System.Windows.Controls.Label frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }

    public static class WpfLabelExtensions
    {
        public static object Content(this WpfLabel me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Content);
        }
    }
}