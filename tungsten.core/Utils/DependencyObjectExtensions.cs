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
    }
}