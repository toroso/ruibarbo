using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace tungsten.core.Utils
{
    internal static class DependencyObjectExtensions
    {
        internal static IEnumerable<FrameworkElement> GetFrameworkElementChildren(this DependencyObject parent)
        {
            var result = new List<FrameworkElement>();

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var asDependencyObject = VisualTreeHelper.GetChild(parent, i);
                var asFrameworkElement = asDependencyObject as FrameworkElement;
                if (asFrameworkElement != null)
                {
                    result.Add(asFrameworkElement);
                }
                else
                {
                    result.AddRange(asDependencyObject.GetFrameworkElementChildren());
                }
            }

            return result;
        }

        internal static FrameworkElement GetFrameworkElementParent(this DependencyObject child)
        {
            DependencyObject current = child;
            while (true)
            {
                current = VisualTreeHelper.GetParent(current);
                if (current == null)
                {
                    return null;
                }

                var asFrameworkElement = current as FrameworkElement;
                if (asFrameworkElement != null)
                {
                    return asFrameworkElement;
                }
            }
        }
    }
}