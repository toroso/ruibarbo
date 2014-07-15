using System.Reflection;

namespace ruibarbo.core.Win32.Factory
{
    public interface IWin32FactoryConfigurator
    {
        void AddRegisteredElementsInAssembly(Assembly assembly);
    }
}