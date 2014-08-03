using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfTextBlockBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TextBlock
    {
        public WpfTextBlockBase(ISearchSourceElement searchParent, TNativeElement textBlock)
            : base(searchParent, textBlock)
        {
        }

        public string Text
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.Text); }
        }
    }
}