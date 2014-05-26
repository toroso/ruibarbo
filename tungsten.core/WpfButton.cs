using System.Windows;
using System.Windows.Threading;

namespace tungsten.core
{
    public class WpfButton : WpfElement
    {
        public WpfButton(Dispatcher dispatcher, FrameworkElement frameworkElement)
            : base(dispatcher, frameworkElement)
        {
        }
    }
}