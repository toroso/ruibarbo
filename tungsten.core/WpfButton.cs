using System.Windows;
using System.Windows.Threading;
using tungsten.core.ElementFactory;

namespace tungsten.core
{
    public class WpfButton : WpfElement
    {
        public WpfButton(Dispatcher dispatcher, IElementFactory elementFactory, FrameworkElement frameworkElement)
            : base(dispatcher, elementFactory, frameworkElement)
        {
        }
    }
}