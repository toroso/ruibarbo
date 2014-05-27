using System.Windows;
using System.Windows.Threading;

namespace tungsten.core.ElementFactory
{
    public interface IElementFactory
    {
        WpfElement CreateWpfElement(Dispatcher dispatcher, SearchSourceElement parent, FrameworkElement element);
    }
}