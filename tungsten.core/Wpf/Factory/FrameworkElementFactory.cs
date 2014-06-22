using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using tungsten.core.ElementFactory;
using tungsten.core.Utils;

namespace tungsten.core.Wpf.Factory
{
    public class FrameworkElementFactory : IElementFactory
    {
        private readonly IDictionary<string, List<Type>> _types = new Dictionary<string, List<Type>>();

        public FrameworkElementFactory(Action<IFrameworkElementFactoryConfigurator> configAction)
        {
            configAction(new FrameworkElementFactoryConfigurator(this));
        }

        internal void AddRegisteredElementsInAssembly(Assembly assembly)
        {
            Type baseType = typeof(IRegisteredElement<>);
            var wpfElementTypes = assembly.GetTypes()
                .Where(t => baseType != t && t.IsSubclassOfGenericInterface(baseType));
            foreach (var type in wpfElementTypes)
            {
                var nativeElementFullName = type.GenericTypeArgumentOf(baseType).FullName;
                var wpfElementType = type;
                AddType(nativeElementFullName, wpfElementType);
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
    }
}