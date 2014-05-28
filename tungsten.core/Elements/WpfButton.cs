using System.Windows;
using System.Windows.Threading;
using tungsten.core.ElementFactory;

namespace tungsten.core.Elements
{
    public class WpfButton : WpfElement
    {
        public WpfButton(Dispatcher dispatcher, IElementFactory elementFactory, SearchSourceElement parent, FrameworkElement frameworkElement)
            : base(dispatcher, elementFactory, parent, frameworkElement)
        {
        }
    }
}