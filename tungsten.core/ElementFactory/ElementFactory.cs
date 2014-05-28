using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.Elements;
using tungsten.core.Utils;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactory : IElementFactory
    {
        private readonly IDictionary<string, Type> _types = new Dictionary<string, Type>();

        public void Add<TFrameworkElement, TWpfElement>()
        {
            _types.Add(typeof(TFrameworkElement).FullName, typeof(TWpfElement));
        }

        public WpfElement CreateWpfElement(Dispatcher dispatcher, SearchSourceElement parent, FrameworkElement element)
        {
            var match = element.GetType()
                .AllTypesInHierarchy()
                .FirstOrDefault(t => _types.ContainsKey(t.FullName));

            if (match != null)
            {
                return (WpfElement)Activator.CreateInstance(_types[match.FullName], dispatcher, this, parent, element);
            }

            // TODO: Better error message, display contents of factory
            throw new InvalidOperationException(string.Format("No type registered for '{0}' or base types.", element.GetType().FullName));
        }
    }
}