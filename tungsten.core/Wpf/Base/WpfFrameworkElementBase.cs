using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Hardware;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Wpf.Base
{
    public abstract class WpfFrameworkElementBase<TNativeElement> : ISearchSourceElement, IAmFoundByUpdatable
        where TNativeElement : System.Windows.FrameworkElement
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

        public virtual string Class
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetType().FullName); }
        }

        public virtual IEnumerable<object> NativeChildren
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.GetFrameworkElementChildren()); }
        }

        public virtual object NativeParent
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

        public bool IsEnabled
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.IsEnabled); }
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

        public TTooltipElement Tooltip<TTooltipElement>()
            where TTooltipElement : class, ISearchSourceElement
        {
            return AllTooltips<TTooltipElement>().FirstOrDefault();
        }

        private IEnumerable<TTooltipElement> AllTooltips<TTooltipElement>()
            where TTooltipElement : class, ISearchSourceElement
        {
            var tooltip = Invoker.Get(this, frameworkElement =>
                {
                    var toolTip = frameworkElement.ToolTip;
                    var asToolTip = toolTip as System.Windows.Controls.ToolTip;
                    if (asToolTip != null)
                    {
                        return asToolTip;
                    }

                    // TODO: This can happen. When? What to do?
                    return null;
                });
            if (tooltip == null)
            {
                return new TTooltipElement[] { };
            }

            return ElementFactory.ElementFactory.CreateElements(this, tooltip)
                .OfType<TTooltipElement>();
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
        public static System.Windows.Rect BoundsOnScreen<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.FrameworkElement
        {
            return Invoker.Get(me, frameworkElement =>
                {
                    var locationFromScreen = frameworkElement.PointToScreen(new System.Windows.Point(0.0, 0.0));
                    var width = frameworkElement.ActualWidth;
                    var height = frameworkElement.ActualHeight;
                    return new System.Windows.Rect(locationFromScreen.X, locationFromScreen.Y, width, height);
                });
        }

        public static bool IsHitTestVisible<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsHitTestVisible);
        }

        public static bool IsKeyboardFocused<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.FrameworkElement
        {
            return Invoker.Get(me, frameworkElement => frameworkElement.IsKeyboardFocused);
        }

        public static void BringIntoView<TNativeElement>(this WpfFrameworkElementBase<TNativeElement> me)
            where TNativeElement : System.Windows.FrameworkElement
        {
            Invoker.Invoke(me, frameworkElement => frameworkElement.BringIntoView());
        }
    }
}