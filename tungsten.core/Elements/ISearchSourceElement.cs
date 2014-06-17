using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public interface ISearchSourceElement
    {
        /// <summary>
        /// Returns the name of this element. In WPF, it is the x:Name and in WinForms it is the Name property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the type that represents this element. In WPF it is a decendant of FrameworkElement and in WinForms it is a
        /// decendand of Control.
        /// </summary>
        Type Class { get; }

        /// <summary>
        /// Returns all elements that this element contains.
        /// TODO: Return IEnumerable of object to support WinForms
        /// </summary>
        IEnumerable<FrameworkElement> NativeChildren { get; }

        /// <summary>
        /// Returns the parent of this element.
        /// TODO: Return object to support WinForms
        /// </summary>
        FrameworkElement NativeParent { get; }

        /// <summary>
        /// Returns the condition that this element was found by.
        /// </summary>
        IEnumerable<By> FoundBys { get; }

        /// <summary>
        /// Return the element that was used as source when finding this element.
        /// </summary>
        ISearchSourceElement SearchParent { get; }

        /// <summary>
        /// A (sort of) unique identifier of this element. Can be used to differentiate two instances of the same element, for instance
        /// in ItemsControls / ListControls.
        /// </summary>
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
        public static IEnumerable<ISearchSourceElement> Children(this ISearchSourceElement me)
        {
            return me.NativeChildren.SelectMany(element => ElementFactory.ElementFactory.CreateWpfElements(me, element));
        }

        /// <summary>
        /// Return a list of possible parents. They all represent the same FrameworkElement, but are wrapped in different
        /// WpfElements.
        /// </summary>
        public static IEnumerable<ISearchSourceElement> Parents(this ISearchSourceElement me)
        {
            var nativeParent = me.NativeParent;
            return nativeParent != null
                ? ElementFactory.ElementFactory.CreateWpfElements(null, nativeParent)
                : new ISearchSourceElement[] { };
        }
    }
}