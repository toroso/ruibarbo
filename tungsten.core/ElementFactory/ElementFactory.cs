using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void Add<TFrameworkElement, TWpfElement>()
        {
            Instance.AddImpl<TFrameworkElement, TWpfElement>();
        }

        private void AddImpl<TFrameworkElement, TWpfElement>()
        {
            var key = typeof(TFrameworkElement).FullName;
            if (_types.ContainsKey(key))
            {
                _types.Remove(key);
            }
            _types.Add(key, typeof(TWpfElement));
        }

        public static WpfElement CreateWpfElement(SearchSourceElement parent, FrameworkElement element)
        {
            return Instance.CreateWpfElementImpl(parent, element);
        }

        private WpfElement CreateWpfElementImpl(SearchSourceElement parent, FrameworkElement element)
        {
            var match = element.GetType()
                .AllTypesInHierarchy()
                .FirstOrDefault(t => _types.ContainsKey(t.FullName));

            if (match != null)
            {
                return (WpfElement)Activator.CreateInstance(_types[match.FullName], parent, element);
            }

            // TODO: Better error message, display contents of factory
            throw new InvalidOperationException(string.Format("No type registered for '{0}' or base types.", element.GetType().FullName));
        }
    }
}