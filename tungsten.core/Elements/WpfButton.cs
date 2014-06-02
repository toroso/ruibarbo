namespace tungsten.core.Elements
{
    public class WpfButton
        : WpfElement<System.Windows.Controls.Button>
    {
        public WpfButton(SearchSourceElement parent, System.Windows.Controls.Button frameworkElement)
            : base(parent, frameworkElement)
        {
        }
    }
}