using System.Reflection;

namespace ruibarbo.core.Wpf.Factory
{
    internal class FrameworkElementFactoryConfigurator : IFrameworkElementFactoryConfigurator
    {
        private readonly FrameworkElementFactory _frameworkElementFactory;

        public FrameworkElementFactoryConfigurator(FrameworkElementFactory frameworkElementFactory)
        {
            _frameworkElementFactory = frameworkElementFactory;
        }

        public void AddRegisteredElementsInAssembly(Assembly assembly)
        {
            _frameworkElementFactory.AddRegisteredElementsInAssembly(assembly);
        }
    }
}