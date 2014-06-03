using System.Reflection;

namespace tungsten.core.ElementFactory
{
    public interface IElementFactoryConfigurator
    {
        void AddElementAssembly(Assembly assembly);
    }
}