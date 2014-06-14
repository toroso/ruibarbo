namespace tungsten.core.Elements
{
    public class WpfButton
        : WpfElement<System.Windows.Controls.Button>, IRegisteredElement<System.Windows.Controls.Button>
    {
        public WpfButton(SearchSourceElement searchParent, System.Windows.Controls.Button frameworkElement)
            : base(searchParent, frameworkElement)
        {
        }
    }
}