using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using tungsten.core.Input;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public abstract class WpfElement<TFrameworkElement> : UntypedWpfElement
        where TFrameworkElement : FrameworkElement
    {
        private readonly WeakReference<TFrameworkElement> _frameworkElement;

        protected WpfElement(SearchSourceElement searchParent, TFrameworkElement frameworkElement)
            : base(searchParent)
        {
            _frameworkElement = new WeakReference<TFrameworkElement>(frameworkElement);
        }

        public override string Name
        {
            get { return Get(frameworkElement => frameworkElement.Name); }
        }

        public override Type Class
        {
            get { return Get(frameworkElement => frameworkElement.GetType()); }
        }

        public bool IsVisible
        {
            get { return Get(frameworkElement => frameworkElement.IsVisible); }
        }

        public bool IsHitTestVisible
        {
            get { return Get(frameworkElement => frameworkElement.IsHitTestVisible); }
        }

        public Rect BoundsOnScreen
        {
            get
            {
                return Get(frameworkElement =>
                {
                    var locationFromScreen = frameworkElement.PointToScreen(new Point(0.0, 0.0));
                    var width = frameworkElement.ActualWidth;
                    var height = frameworkElement.ActualHeight;
                    return new Rect(locationFromScreen.X, locationFromScreen.Y, width, height);
                });
            }
        }

        public override IEnumerable<UntypedWpfElement> Children
        {
            get
            {
                // TODO: Retry a few times if none is found
                var frameworkElementChildren = Get(frameworkElement => frameworkElement.GetFrameworkElementChildren());
                return frameworkElementChildren.Select(CreateWpfElement);
            }
        }

        public override UntypedWpfElement Parent
        {
            get
            {
                var rootFrameworkElement = Get(frameworkElement =>
                    {
                        DependencyObject current = frameworkElement;
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
                    });

                return rootFrameworkElement != null
                    ? CreateWpfElement(null, rootFrameworkElement)
                    : null;
            }
        }

        public bool IsKeyboardFocused
        {
            get { return Get(frameworkElement => frameworkElement.IsKeyboardFocused); }
        }

        protected void Invoke(Action<TFrameworkElement> action)
        {
            var strongReference = GetStrongReference();
            Invoker.Invoke(() => action(strongReference));
        }

        protected TRet Get<TRet>(Func<TFrameworkElement, TRet> func)
        {
            var strongReference = GetStrongReference();
            return Invoker.Get(() => func(strongReference));
        }

        public void Click()
        {
            var bounds = BoundsOnScreen;
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }

        private TFrameworkElement GetStrongReference()
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