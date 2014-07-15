using System.Reflection;

namespace ruibarbo.core.Wpf.Factory
{
    public interface IFrameworkElementFactoryConfigurator
    {
        void AddRegisteredElementsInAssembly(Assembly assembly);
    }
}