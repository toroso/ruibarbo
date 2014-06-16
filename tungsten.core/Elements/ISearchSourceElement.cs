using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public interface ISearchSourceElement
    {
        string Name { get; }
        Type Class { get; }

        IEnumerable<FrameworkElement> NativeChildren { get; } // TODO: Return IEnumerable<object>
        FrameworkElement NativeParent { get; } // TODO: Return object
        
        IEnumerable<By> SearchConditions { get; }
        ISearchSourceElement SearchParent { get; }

        int InstanceId { get; }
    }

    public static class SearchSourceElementExtensions
    {
        public static IEnumerable<ISearchSourceElement> ElementPath(this ISearchSourceElement me)
        {
            if (me.SearchParent != null)
            {
                foreach (var ancestor in me.SearchParent.ElementPath())
                {
                    yield return ancestor;
                }
            }
            yield return me;
        }

        /// <summary>
        /// Return a list of possible children. The same FrameworkElement might appear several time but wrapped in different WpfElements.
        /// TODO: Make into extension method
        /// </summary>
        public static IEnumerable<UntypedWpfElement> Children(this ISearchSourceElement me)
        {
            return me.NativeChildren.SelectMany(element => ElementFactory.ElementFactory.CreateWpfElements(me, element));
        }

        /// <summary>
        /// Return a list of possible parents. They all represent the same FrameworkElement, but are wrapped in different
        /// WpfElements.
        /// </summary>
        public static IEnumerable<UntypedWpfElement> Parents(this ISearchSourceElement me)
        {
            var nativeParent = me.NativeParent;
            return nativeParent != null
                ? ElementFactory.ElementFactory.CreateWpfElements(null, nativeParent)
                : new UntypedWpfElement[] { };
        }
    }
}