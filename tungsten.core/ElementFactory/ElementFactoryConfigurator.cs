using System.Windows;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactoryConfigurator : IElementFactoryConfigurator
    {
        public IElementFactoryForConfigurator For<TFrameworkElement>() where TFrameworkElement : FrameworkElement
        {
            return new ElementFactoryForConfigurator<TFrameworkElement>();
        }
    }
}