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

        private readonly IDictionary<string, Type> _types = new Dictionary<string, Type>();

        public static void AddAssembly(Assembly assembly)
        {
            Instance.AddAssemblyImpl(assembly);
        }

        private void AddAssemblyImpl(Assembly assembly)
        {
            Type baseType = typeof(WpfElement<>);
            var wpfElementTypes = assembly.GetTypes()
                .Where(t => baseType != t && t.IsSubclassOfGeneric(baseType));
            foreach (var type in wpfElementTypes)
            {
                var frameworkElementFullName = type.GenericTypeArgumentOf(baseType).FullName;
                var wpfElementType = type;
                AddType(frameworkElementFullName, wpfElementType);
            }
        }

        private void AddType(string frameworkElementFullName, Type wpfElementType)
        {
            if (_types.ContainsKey(frameworkElementFullName))
            {
                _types.Remove(frameworkElementFullName);
            }

            _types.Add(frameworkElementFullName, wpfElementType);
        }

        public static UntypedWpfElement CreateWpfElement(SearchSourceElement parent, FrameworkElement element)
        {
            return Instance.CreateWpfElementImpl(parent, element);
        }

        private UntypedWpfElement CreateWpfElementImpl(SearchSourceElement parent, FrameworkElement element)
        {
            var match = element.GetType()
                .AllTypesInHierarchy()
                .FirstOrDefault(t => _types.ContainsKey(t.FullName));

            if (match != null)
            {
                return (UntypedWpfElement)Activator.CreateInstance(_types[match.FullName], parent, element);
            }

            // TODO: Better error message, display contents of factory
            throw new InvalidOperationException(string.Format("No type registered for '{0}' or base types.", element.GetType().FullName));
        }
    }
}