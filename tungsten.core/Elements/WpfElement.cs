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
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Name); }
        }

        public override Type Class
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetType()); }
        }

        public bool IsVisible
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsVisible); }
        }

        public bool IsHitTestVisible
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsHitTestVisible); }
        }

        public Rect BoundsOnScreen
        {
            get
            {
                return Invoker.Get(this, frameworkElement =>
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
                var frameworkElementChildren = Invoker.Get(this, frameworkElement => frameworkElement.GetFrameworkElementChildren());
                return frameworkElementChildren.Select(CreateWpfElement);
            }
        }

        public override UntypedWpfElement Parent
        {
            get
            {
                var rootFrameworkElement = Invoker.Get(this, frameworkElement =>
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
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsKeyboardFocused); }
        }

        public void Click()
        {
            var bounds = BoundsOnScreen;
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }

        internal TFrameworkElement GetStrongReference()
        {
            TFrameworkElement strongReference;
            if (_frameworkElement.TryGetTarget(out strongReference))
            {
                return strongReference;
            }

            throw ManglaException.NoLongerAvailable(this);
        }
    }
}