using System.Reflection;

namespace ruibarbo.core.Win32.Factory
{
    internal class Win32FactoryConfigurator : IWin32FactoryConfigurator
    {
        private readonly Win32ControlFactory _win32ControlFactory;

        public Win32FactoryConfigurator(Win32ControlFactory win32ControlFactory)
        {
            _win32ControlFactory = win32ControlFactory;
        }

        public void AddRegisteredElementsInAssembly(Assembly assembly)
        {
            _win32ControlFactory.AddRegisteredElementsInAssembly(assembly);
        }
    }
}