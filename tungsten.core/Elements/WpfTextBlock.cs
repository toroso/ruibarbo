namespace tungsten.core.Elements
{
    public class WpfTextBlock : WpfElement<System.Windows.Controls.TextBlock>
    {
        public WpfTextBlock(SearchSourceElement searchParent, System.Windows.Controls.TextBlock textBlock)
            : base(searchParent, textBlock)
        {
        }
    }

    public static class WpfTextBlockExtensions
    {
        public static string Text(this WpfTextBlock me)
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Text);
        }
    }
}