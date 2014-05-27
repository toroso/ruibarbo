using System.Windows;
using System.Windows.Threading;
using tungsten.core.ElementFactory;

namespace tungsten.core
{
    public class WpfWindow : WpfElement
    {
        public WpfWindow(Dispatcher dispatcher, IElementFactory elementFactory, FrameworkElement frameworkElement)
            : base(dispatcher, elementFactory, frameworkElement)
        {
        }
    }
}