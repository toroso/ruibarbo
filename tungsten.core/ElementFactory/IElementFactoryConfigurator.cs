using System.Windows;

namespace tungsten.core.ElementFactory
{
    public interface IElementFactoryConfigurator
    {
        IElementFactoryForConfigurator For<TFrameworkElement>() where TFrameworkElement : FrameworkElement;
    }
}