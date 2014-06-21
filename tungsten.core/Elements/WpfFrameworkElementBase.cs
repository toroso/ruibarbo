using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using tungsten.core.Input;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public abstract class WpfFrameworkElementBase<TNativeElement> : ISearchSourceElement, IAmFoundByUpdatable
        where TNativeElement : FrameworkElement
    {
        private readonly ISearchSourceElement _searchParent;
        private readonly WeakReference<TNativeElement> _frameworkElement;
        private By[] _bys;

        protected WpfFrameworkElementBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
        {
            _searchParent = searchParent;
            _frameworkElement = new WeakReference<TNativeElement>(frameworkElement);
            _bys = new By[] { };
        }

        public virtual string Name
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Name); }
        }

        public virtual Type Class
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetType()); }
        }

        public virtual IEnumerable<FrameworkElement> NativeChildren
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetFrameworkElementChildren()); }
        }

        public virtual FrameworkElement NativeParent
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetFrameworkElementParent()); }
        }

        public virtual IEnumerable<By> FoundBys
        {
            get { return _bys; }
        }

        public virtual ISearchSourceElement SearchParent
        {
            get { return _searchParent; }
        }

        public virtual int InstanceId
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetHashCode()); }
        }

        public bool IsVisible
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsVisible); }
        }

        public virtual void Click()
        {
            this.BringIntoView();

            var bounds = this.BoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }

        public void FoundBy(IEnumerable<By> bys)
        {
            _bys = bys.AppendByClass(Class).ToArray();
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

        public static void BringIntoView<TNativeElement>(this WpfFrameworkElementBase<TNativeElement> me)
            where TNativeElement : FrameworkElement
        {
            Invoker.Invoke(me, frameworkElement => frameworkElement.BringIntoView());
        }
    }
}