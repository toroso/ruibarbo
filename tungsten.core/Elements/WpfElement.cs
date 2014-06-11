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

        public override int ReferenceId
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetHashCode()); }
        }

        public override string Name
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Name); }
        }

        public override Type Class
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetType()); }
        }

        public override IEnumerable<UntypedWpfElement> Children
        {
            get { return FrameworkElementChildren.SelectMany(CreateWpfElements); }
        }

        public override IEnumerable<FrameworkElement> FrameworkElementChildren
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetFrameworkElementChildren()); }
        }

        public override IEnumerable<UntypedWpfElement> Parents
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
                    ? CreateWpfElements(null, rootFrameworkElement)
                    : new UntypedWpfElement[] { };
            }
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

    public static class WpfElementExtensions
    {
        public static Rect BoundsOnScreen<TFrameworkElement>(this WpfElement<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            return Invoker.Get(me, frameworkElement =>
                {
                    var locationFromScreen = frameworkElement.PointToScreen(new Point(0.0, 0.0));
                    var width = frameworkElement.ActualWidth;
                    var height = frameworkElement.ActualHeight;
                    return new Rect(locationFromScreen.X, locationFromScreen.Y, width, height);
                });
        }

        public static bool IsHitTestVisible<TFrameworkElement>(this WpfElement<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsHitTestVisible);
        }

        public static bool IsKeyboardFocused<TFrameworkElement>(this WpfElement<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsKeyboardFocused);
        }

        public static bool IsVisible<TFrameworkElement>(this WpfElement<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsVisible);
        }

        public static void Click<TFrameworkElement>(this WpfElement<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            var bounds = me.BoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }
    }
}