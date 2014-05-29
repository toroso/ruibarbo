using System.Windows;
using System.Windows.Threading;

namespace tungsten.core.Elements
{
    public class WpfWindow : WpfElement
    {
        public WpfWindow(Dispatcher dispatcher, SearchSourceElement parent, FrameworkElement frameworkElement)
            : base(dispatcher, parent, frameworkElement)
        {
        }
    }
}