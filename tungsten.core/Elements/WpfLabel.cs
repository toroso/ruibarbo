namespace tungsten.core.Elements
{
    public class WpfLabel : WpfLabelBase<System.Windows.Controls.Label>, IRegisteredElement<System.Windows.Controls.Label>
    {
        public WpfLabel(SearchSourceElement searchParent, System.Windows.Controls.Label frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}