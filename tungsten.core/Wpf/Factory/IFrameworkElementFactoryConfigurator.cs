using System.Reflection;

namespace tungsten.core.Wpf.Factory
{
    public interface IFrameworkElementFactoryConfigurator
    {
        void AddRegisteredElementsInAssembly(Assembly assembly);
    }
}