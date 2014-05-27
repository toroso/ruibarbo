using System.Windows;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactoryConfigurator : IElementFactoryConfigurator
    {
        private readonly ElementFactory _elementFactory;

        public ElementFactoryConfigurator(ElementFactory elementFactory)
        {
            _elementFactory = elementFactory;
        }

        public IElementFactoryForConfigurator For<TFrameworkElement>() where TFrameworkElement : FrameworkElement
        {
            return new ElementFactoryForConfigurator<TFrameworkElement>(_elementFactory);
        }
    }
}