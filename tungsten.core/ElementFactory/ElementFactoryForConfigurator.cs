using System.Windows;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactoryForConfigurator<TFrameworkElement> : IElementFactoryForConfigurator
        where TFrameworkElement : FrameworkElement
    {
        private readonly ElementFactory _elementFactory;

        public ElementFactoryForConfigurator(ElementFactory elementFactory)
        {
            _elementFactory = elementFactory;
        }

        public void Create<TWpfElement>() where TWpfElement : WpfElement
        {
            _elementFactory.Add<TFrameworkElement, TWpfElement>();
        }
    }
}