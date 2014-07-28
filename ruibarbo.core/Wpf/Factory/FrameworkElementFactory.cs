using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Utils;

namespace ruibarbo.core.Wpf.Factory
{
    public sealed class FrameworkElementFactory : IElementFactory
    {
        private readonly IDictionary<string, List<Type>> _types = new Dictionary<string, List<Type>>();

        public FrameworkElementFactory(Action<IFrameworkElementFactoryConfigurator> configAction)
        {
            configAction(new FrameworkElementFactoryConfigurator(this));
        }

        internal void AddRegisteredElementsInAssembly(Assembly assembly)
        {
            var registeredElementTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(RegisteredElementAttribute), true).Any());
            foreach (var elementType in registeredElementTypes)
            {
                var baseType = elementType.BaseType;
                var nativeElement = baseType.GetGenericArguments()[0];
                AddType(nativeElement.FullName, elementType);
            }
        }

        private void AddType(string frameworkElementFullName, Type wpfElementType)
        {
            if (!_types.ContainsKey(frameworkElementFullName))
            {
                _types[frameworkElementFullName] = new List<Type>();
            }

            var types = _types[frameworkElementFullName];
            types.Add(wpfElementType);
        }

        public IEnumerable<ISearchSourceElement> CreateElements(ISearchSourceElement parent, object nativeObject)
        {
            return
                nativeObject
                    .GetType()
                    .AllTypesInHierarchy()
                    .Where(nativeType => _types.ContainsKey(nativeType.FullName))
                    .SelectMany(nativeType => _types[nativeType.FullName]
                        .Select(wpfElementType => (ISearchSourceElement)Activator.CreateInstance(wpfElementType, parent, nativeObject)));
        }

        public IEnumerable<object> GetRootElements()
        {
            // Application.Windows only returns Windows that are run on the same thread as Application. NonAppWindowsInternal
            // holds Windows run on other threads (non-documented, as an internal property).
            var application = Application.Current;
            var windowsOnOtherThreads = (WindowCollection)application
                .GetType()
                .GetProperty("NonAppWindowsInternal", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .GetValue(application, null);
            return windowsOnOtherThreads.Cast<Window>();
        }
    }
}