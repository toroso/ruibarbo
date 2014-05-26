using System.Windows;
using System.Windows.Threading;

namespace tungsten.core
{
    public class WpfWindow : WpfElement
    {
        public WpfWindow(Dispatcher dispatcher, FrameworkElement frameworkElement)
            : base(dispatcher, frameworkElement)
        {
        }
    }
}