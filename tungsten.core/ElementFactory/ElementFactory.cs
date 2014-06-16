using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using tungsten.core.Elements;
using tungsten.core.Utils;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactory
    {
        private static readonly ThreadLocal<ElementFactory> Instances = new ThreadLocal<ElementFactory>();

        private static ElementFactory Instance
        {
            get
            {
                if (!Instances.IsValueCreated)
                {
                    Instances.Value = new ElementFactory();
                }
                return Instances.Value;
            }
        }

        private readonly IDictionary<string, List<Type>> _types = new Dictionary<string, List<Type>>();

        public static void AddAssembly(Assembly assembly)
        {
            Instance.AddAssemblyImpl(assembly);
        }

        private void AddAssemblyImpl(Assembly assembly)
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
            if (!types.Contains(wpfElementType))
            {
                types.Add(wpfElementType);
            }
        }

        /// <summary>
        /// Creates matching elements. More than one element may match the FrameworkElement type, so a list is returned. The
        /// client must filter out the most interesting one.
        /// </summary>
        public static IEnumerable<UntypedWpfElement> CreateWpfElements(ISearchSourceElement parent, object nativeElement)
        {
            return Instance.CreateWpfElementsImpl(parent, nativeElement);
        }

        private IEnumerable<UntypedWpfElement> CreateWpfElementsImpl(ISearchSourceElement parent, object nativeElement)
        {
            return nativeElement.GetType()
                .AllTypesInHierarchy()
                .Where(nativeType => _types.ContainsKey(nativeType.FullName))
                .SelectMany(nativeType => _types[nativeType.FullName]
                    .Select(wpfElementType => (UntypedWpfElement)Activator.CreateInstance(wpfElementType, parent, nativeElement)));
        }
    }
}