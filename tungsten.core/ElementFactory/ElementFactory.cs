using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        private readonly List<IElementFactory> _factories = new List<IElementFactory>();

        public static void AddFactory(IElementFactory factory)
        {
            Instance.AddFactoryImpl(factory);
        }

        private void AddFactoryImpl(IElementFactory factory)
        {
            _factories.Add(factory);
        }

        public static void RemoveAllFactories()
        {
            Instance.RemoveAllFactoriesImpl();
        }

        private void RemoveAllFactoriesImpl()
        {
            _factories.Clear();
        }

        /// <summary>
        /// Creates matching elements. More than one element may match the native type, so a list is returned. The
        /// client must filter out the most interesting one.
        /// </summary>
        public static IEnumerable<ISearchSourceElement> CreateElements(ISearchSourceElement parent, object nativeObject)
        {
            return Instance.CreateElementsImpl(parent, nativeObject);
        }

        private IEnumerable<ISearchSourceElement> CreateElementsImpl(ISearchSourceElement parent, object nativeObject)
        {
            return _factories.SelectMany(f => f.CreateElements(parent, nativeObject));
        }

        public static IEnumerable<object> GetRootElements()
        {
            return Instance.GetRootElementsImpl();
        }

        private IEnumerable<object> GetRootElementsImpl()
        {
            return _factories.SelectMany(f => f.GetRootElements());
        }
    }
}