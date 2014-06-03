using System.Reflection;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactoryConfigurator : IElementFactoryConfigurator
    {
        public void AddElementAssembly(Assembly assembly)
        {
            ElementFactory.AddAssembly(assembly);
        }
    }
}