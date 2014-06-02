using System.Windows;

namespace tungsten.core.Elements
{
    public class WpfWindow : WpfElement
    {
        public WpfWindow(SearchSourceElement parent, FrameworkElement frameworkElement)
            : base(parent, frameworkElement)
        {
            var strongReference = GetFrameworkElement<System.Windows.Window>();
            Invoker.Invoke(() => strongReference.Activate());
        }
    }
}