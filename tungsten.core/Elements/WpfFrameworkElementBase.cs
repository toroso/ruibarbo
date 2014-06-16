using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using tungsten.core.Input;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public abstract class WpfFrameworkElementBase<TNativeElement> : UntypedWpfElement
        where TNativeElement : FrameworkElement
    {
        private readonly WeakReference<TNativeElement> _frameworkElement;

        protected WpfFrameworkElementBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
            : base(searchParent)
        {
            _frameworkElement = new WeakReference<TNativeElement>(frameworkElement);
        }

        public override string Name
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Name); }
        }

        public override Type Class
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetType()); }
        }

        public override IEnumerable<FrameworkElement> NativeChildren
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetFrameworkElementChildren()); }
        }

        public override FrameworkElement NativeParent
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetFrameworkElementParent()); }
        }

        public override int InstanceId
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetHashCode()); }
        }

        internal TNativeElement GetStrongReference()
        {
            TNativeElement strongReference;
            if (_frameworkElement.TryGetTarget(out strongReference))
            {
                return strongReference;
            }

            throw ManglaException.NoLongerAvailable(this);
        }
    }

    public static class WpfElementExtensions
    {
        public static Rect BoundsOnScreen<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
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

        public static bool IsHitTestVisible<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsHitTestVisible);
        }

        public static bool IsKeyboardFocused<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsKeyboardFocused);
        }

        public static bool IsVisible<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsVisible);
        }

        public static void BringIntoView<TNativeElement>(this WpfFrameworkElementBase<TNativeElement> me)
            where TNativeElement : FrameworkElement
        {
            Invoker.Invoke(me, frameworkElement => frameworkElement.BringIntoView());
        }

        public static void Click<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : FrameworkElement
        {
            me.BringIntoView();

            var bounds = me.BoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }
    }
}