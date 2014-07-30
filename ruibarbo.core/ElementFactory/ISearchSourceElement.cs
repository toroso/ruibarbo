using System.Collections.Generic;

namespace ruibarbo.core.ElementFactory
{
    public interface ISearchSourceElement
    {
        /// <summary>
        /// Returns the name of this element. In WPF, it is the x:Name and in WinForms it is the Name property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns a string representation of the type of this element. In WPF it is the class FullName of FrameworkElement and in Win32
        /// it is the Window Class name.
        /// </summary>
        string Class { get; }

        /// <summary>
        /// Returns all elements that this element contains.
        /// </summary>
        IEnumerable<object> NativeChildren { get; }

        /// <summary>
        /// Returns the parent of this element.
        /// </summary>
        object NativeParent { get; }

        /// <summary>
        /// Returns the condition that this element was found by.
        /// </summary>
        string FoundBy { get; }

        /// <summary>
        /// Return the element that was used as source when finding this element.
        /// </summary>
        ISearchSourceElement SearchParent { get; }

        /// <summary>
        /// A (sort of) unique identifier of this element. Can be used to differentiate two instances of the same element, for instance
        /// in ItemsControls / ListControls.
        /// </summary>
        int InstanceId { get; }

        bool IsVisible { get; }
        bool IsEnabled { get; }
        void Click();
        void DoubleClick();
    }
}