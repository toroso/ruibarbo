using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using tungsten.core.Input;

namespace tungsten.core.Elements
{
    public class WpfElement<TFrameworkElement> : UntypedWpfElement
        where TFrameworkElement : FrameworkElement
    {
        private readonly WeakReference<TFrameworkElement> _frameworkElement;

        public WpfElement(SearchSourceElement parent, TFrameworkElement frameworkElement)
            : base(parent)
        {
            _frameworkElement = new WeakReference<TFrameworkElement>(frameworkElement);
        }

        public override string Name
        {
            get
            {
                var strongReference = GetFrameworkElement();
                return Invoker.Get(() => strongReference.Name);
            }
        }

        public override Type Class
        {
            get
            {
                var strongReference = GetFrameworkElement();
                return strongReference.GetType();
            }
        }

        public override IEnumerable<UntypedWpfElement> Children
        {
            get
            {
                var strongReference = GetFrameworkElement();
                // TODO: Retry a few times if none is found
                var frameworkElementChildren = GetFrameworkElementChildren(strongReference);
                return frameworkElementChildren.Select(CreateWpfElement);
            }
        }

        public bool IsKeyboardFocused
        {
            get
            {
                var strongReference = GetFrameworkElement();
                return Invoker.Get(() => strongReference.IsKeyboardFocused);
            }
        }

        private IEnumerable<FrameworkElement> GetFrameworkElementChildren(DependencyObject parent)
        {
            var result = new List<FrameworkElement>();

            int count = Invoker.Get(() => VisualTreeHelper.GetChildrenCount(parent));
            for (int i = 0; i < count; i++)
            {
                var asDependencyObject = Invoker.Get(() => VisualTreeHelper.GetChild(parent, i));
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
            var locationFromScreen = Invoker.Get(() => strongReference.PointToScreen(new Point(0.0, 0.0)));
            var width = Invoker.Get(() => strongReference.ActualWidth);
            var height = Invoker.Get(() => strongReference.ActualHeight);

            var centerX = (int) (locationFromScreen.X + width/2);
            var centerY = (int) (locationFromScreen.Y + height/2);

            Mouse.Click(centerX, centerY);
        }

        protected TFrameworkElement GetFrameworkElement()
        {
            TFrameworkElement strongReference;
            if (_frameworkElement.TryGetTarget(out strongReference))
            {
                return strongReference;
            }

            // No longer available
            // TODO: Use assertion exception, created by injected factory
            // TODO: Better message. Perhaps search conditions (By:s), including parents' search conditions.
            throw new Exception("Framework element is no longer available");
        }
    }
}