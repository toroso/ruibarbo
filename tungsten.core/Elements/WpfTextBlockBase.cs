namespace tungsten.core.Elements
{
    public class WpfTextBlockBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TextBlock
    {
        public WpfTextBlockBase(SearchSourceElement searchParent, TNativeElement textBlock)
            : base(searchParent, textBlock)
        {
        }
    }

    public static class WpfTextBlockBaseExtensions
    {
        public static string Text<TNativeElement>(this WpfTextBlockBase<TNativeElement> me)
            where TNativeElement : System.Windows.Controls.TextBlock
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.Text);
        }
    }
}