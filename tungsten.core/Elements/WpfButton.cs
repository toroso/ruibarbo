using System.Windows;
using System.Windows.Threading;

namespace tungsten.core.Elements
{
    public class WpfButton : WpfElement
    {
        public WpfButton(Dispatcher dispatcher, SearchSourceElement parent, FrameworkElement frameworkElement)
            : base(dispatcher, parent, frameworkElement)
        {
        }
    }
}