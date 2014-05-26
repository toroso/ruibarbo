using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace tungsten.core
{
    public class WpfElement : SearchSourceElement
    {
        private readonly WeakReference<FrameworkElement> _frameworkElement; // TODO: Weak reference

        public WpfElement(FrameworkElement frameworkElement, Dispatcher dispatcher)
            : base(dispatcher)
        {
            _frameworkElement = new WeakReference<FrameworkElement>(frameworkElement);
        }

        public string Name
        {
            get
            {
                var strongReference = GetFrameworkElement();
                return GetDispatched(() => strongReference.Name);
            }
        }

        public Type Class
        {
            get { return _frameworkElement.GetType(); }
        }

        public override IEnumerable<WpfElement> Children
        {
            get
            {
                var strongReference = GetFrameworkElement();
                // TODO: Retry a few times if none is found
                var frameworkElementChildren = GetFrameworkElementChildren(strongReference);
                return frameworkElementChildren.Select(x => new WpfElement(x, Dispatcher)); // TODO: Factory that creates types
            }
        }

        private IEnumerable<FrameworkElement> GetFrameworkElementChildren(DependencyObject parent)
        {
            var result = new List<FrameworkElement>();

            int count = GetDispatched(() => VisualTreeHelper.GetChildrenCount(parent));
            for (int i = 0; i < count; i++)
            {
                var asDependencyObject = GetDispatched(() => VisualTreeHelper.GetChild(parent, i));
                var asFrameworkElement = asDependencyObject as FrameworkElement;
                if (asFrameworkElement != null)
                {
                    result.Add(asFrameworkElement);
                }
                else
                {
                    result.AddRange(GetFrameworkElementChildren(asDependencyObject));
                }
            }

            return result;
        }

        public void Click()
        {
            var strongReference = GetFrameworkElement();
            var locationFromWindow = GetDispatched(() => strongReference.TranslatePoint(new Point(0.0, 0.0), null));
            var locationFromScreen = GetDispatched(() => strongReference.PointToScreen(locationFromWindow));
            var width = GetDispatched(() => strongReference.ActualWidth);
            var height = GetDispatched(() => strongReference.ActualHeight);

            var centerX = (int) (locationFromScreen.X + width/2);
            var centerY = (int) (locationFromScreen.Y + height/2);

            Mouse.Click(centerX, centerY);
        }

        private FrameworkElement GetFrameworkElement()
        {
            FrameworkElement strongReference;
            if (_frameworkElement.TryGetTarget(out strongReference))
            {
                return strongReference;
            }

            // No longer available
            // TODO: Use assertion exception, created by injected factory
            // TODO: Better message. Perhaps search conditions (By:s).
            throw new Exception("Framework element is no longer available");
        }
    }
}