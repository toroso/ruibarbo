using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace tungsten.core.ElementFactory
{
    internal class ElementFactory : IElementFactory
    {
        private readonly IDictionary<string, Type> _types = new Dictionary<string, Type>();

        public void Add<TFrameworkElement, TWpfElement>()
        {
            _types.Add(typeof(TFrameworkElement).FullName, typeof(TWpfElement));
        }

        public WpfElement CreateWpfElement(Dispatcher dispatcher, FrameworkElement element)
        {
            Type current = element.GetType();
            while (current != null)
            {
                if (_types.ContainsKey(current.FullName))
                {
                    return (WpfElement)Activator.CreateInstance(_types[current.FullName], dispatcher, this, element);
                }

                current = current.BaseType;
            }

            // TODO: Better error message, display contents of factory
            throw new InvalidOperationException(string.Format("No type registered for '{0}' or base types.", element.GetType().FullName));
        }
    }
}