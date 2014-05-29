using System.Windows;
using tungsten.core.Elements;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactoryForConfigurator<TFrameworkElement> : IElementFactoryForConfigurator
        where TFrameworkElement : FrameworkElement
    {
        public void Create<TWpfElement>() where TWpfElement : WpfElement
        {
            ElementFactory.Add<TFrameworkElement, TWpfElement>();
        }
    }
}