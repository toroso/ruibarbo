using System;
using System.Collections.Generic;
using System.Linq;
using tungsten.core.Hardware;
using tungsten.core.Search;
using tungsten.core.Utils;

namespace tungsten.core.Wpf.Base
{
    public abstract class WpfFrameworkElementBase<TNativeElement> : ISearchSourceElement, IAmFoundByUpdatable, IClickable
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
            Mouse.Click(this);
        }

        public MousePoint ClickablePoint
        {
            get
            {
                var bounds = this.BoundsOnScreen();
                var centerX = (int)(bounds.X + bounds.Width / 2);
                var centerY = (int)(bounds.Y + bounds.Height / 2);
                return new MousePoint(centerX, centerY);
            }
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
            System.Windows.Controls.ToolTip tooltip = Invoker.Get(this, frameworkElement =>
                {
                    object toolTip = frameworkElement.ToolTip;
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
                .OfType<TTooltipElement>()
                .Where(e => e.GetType() == typeof(TTooltipElement));
        }

        // I would have liked a method that always returns the visual tree of the ToolTip. However, there seems to be no way of
        // getting hold on it when the ToolTip is specified as a string. The visual is created on the fly (after the ToolTipOpening
        // event has fired) and is opened in a Popup window that has no relation to the FrameworkElement. This seems to be the only
        // way, to have two separate methods for the two scenarios...
        //
        // One possibility, based on http://stackoverflow.com/a/24596864/617658:
        // Create a ShowTooltip_T() method that places the mouse over the FrameworkElement. Then set an attach property on the ToolTip
        // style (a uniqe value such as timestamp) and extract the visuals from the attached property callback.
        public string TooltipAsString()
        {
            return Invoker.Get(this, frameworkElement =>
                {
                    var toolTip = frameworkElement.ToolTip;
                    var asString = toolTip as string;
                    if (asString != null)
                    {
                        return asString;
                    }

                    return null;
                });
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
            bool isInView = Wait.Until(me.IsInView);
            if (!isInView)
            {
                throw ManglaException.StateFailed(me, x => x.IsInView());
            }
        }

        public static bool IsInView<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.FrameworkElement
        {
            return Invoker.Get(me, element =>
                {
                    var container = System.Windows.Media.VisualTreeHelper.GetParent(element) as System.Windows.FrameworkElement;
                    if (container == null)
                    {
                        // Not really sure about this...
                        return true;
                    }

                    var bounds = element
                        .TransformToAncestor(container)
                        .TransformBounds(new System.Windows.Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
                    var rect = new System.Windows.Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
                    return rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight);
                });
        }
    }
}