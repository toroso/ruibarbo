using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
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
            _types.Add(typeof(TFrameworkElement).FullName, typeof(TWpfElement));
        }

        public static WpfElement CreateWpfElement(Dispatcher dispatcher, SearchSourceElement parent, FrameworkElement element)
        {
            return Instance.CreateWpfElementImpl(dispatcher, parent, element);
        }

        private WpfElement CreateWpfElementImpl(Dispatcher dispatcher, SearchSourceElement parent, FrameworkElement element)
        {
            var match = element.GetType()
                .AllTypesInHierarchy()
                .FirstOrDefault(t => _types.ContainsKey(t.FullName));

            if (match != null)
            {
                return (WpfElement)Activator.CreateInstance(_types[match.FullName], dispatcher, parent, element);
            }

            // TODO: Better error message, display contents of factory
            throw new InvalidOperationException(string.Format("No type registered for '{0}' or base types.", element.GetType().FullName));
        }
    }
}