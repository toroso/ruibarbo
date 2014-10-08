using System;
using System.Collections.Generic;
using System.Linq;
using ruibarbo.core.Common;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Hardware;
using ruibarbo.core.Search;
using ruibarbo.core.Utils;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public abstract class WpfFrameworkElementBase<TNativeElement> : ISearchSourceElement, IAmFoundByUpdatable, IHasStrongReference<TNativeElement>, IClickable
        where TNativeElement : System.Windows.FrameworkElement
    {
        private readonly ISearchSourceElement _searchParent;
        private readonly WeakReference _frameworkElement;
        private string _foundByAsString;

        protected WpfFrameworkElementBase(ISearchSourceElement searchParent, TNativeElement frameworkElement)
        {
            _searchParent = searchParent;
            _frameworkElement = new WeakReference(frameworkElement);
            _foundByAsString = string.Empty;
        }

        public virtual string Name
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.Name); }
        }

        public virtual string Class
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.GetType().FullName); }
        }

        public virtual IEnumerable<object> NativeChildren
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.GetFrameworkElementChildren()); }
        }

        public virtual object NativeParent
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.GetFrameworkElementParent()); }
        }

        public virtual string FoundBy
        {
            get { return _foundByAsString; }
        }

        public virtual ISearchSourceElement SearchParent
        {
            get { return _searchParent; }
        }

        public virtual int InstanceId
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.GetHashCode()); }
        }

        public bool IsVisible
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsVisible); }
        }

        public bool IsEnabled
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsEnabled); }
        }

        public virtual void Click()
        {
            Click(x => { });
        }

        public virtual void Click(Action<Configurator> cfgAction)
        {
            VerifyIsLoadedAndVisibleAndEnabled();
            this.BringIntoView();
            VerifyIsClickable();
            Mouse.Click(this, cfgAction);
        }

        public void DoubleClick()
        {
            VerifyIsLoadedAndVisibleAndEnabled();
            this.BringIntoView();
            VerifyIsClickable();
            Mouse.DoubleClick(this);
        }

        private void VerifyIsLoadedAndVisibleAndEnabled()
        {
            bool isLoaded = Wait.Until(() => IsLoaded);
            if (!isLoaded)
            {
                throw RuibarboException.StateFailed(this, x => x.IsLoaded);
            }

            bool isVisible = Wait.Until(() => IsVisible);
            if (!isVisible)
            {
                throw RuibarboException.StateFailed(this, x => x.IsVisible);
            }

            bool isEnabled = Wait.Until(() => IsEnabled);
            if (!isEnabled)
            {
                throw RuibarboException.StateFailed(this, x => x.IsEnabled);
            }
        }

        private bool IsLoaded
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.IsLoaded); }
        }

        private void VerifyIsClickable()
        {
            bool isClickable = Wait.Until(() => IsClickable);
            if (!isClickable)
            {
                var clickablePoint = ClickablePoint;
                var screenPoint = new System.Windows.Point(clickablePoint.X, clickablePoint.Y);
                var localPoint = OnUiThread.Get(this, frameworkElement => frameworkElement.PointFromScreen(screenPoint));
                throw RuibarboException.StateFailed(this, x => x.IsClickable, string.Format("Clickable Point: {0}", localPoint));
            }
        }

        public bool IsClickable
        {
            get
            {
                var clickablePoint = ClickablePoint;
                var screenPoint = new System.Windows.Point(clickablePoint.X, clickablePoint.Y);
                return OnUiThread.Get(this, frameworkElement =>
                    {
                        System.Windows.FrameworkElement rootElement = GetRootElement(frameworkElement);
                        var localPoint = rootElement.PointFromScreen(screenPoint);
                        System.Windows.Media.HitTestResult result = null;
                        System.Windows.Media.VisualTreeHelper.HitTest(
                            rootElement,
                            filterElement =>
                                {
                                    var uiElement = filterElement as System.Windows.UIElement;
                                    if (uiElement != null)
                                    {
                                        if (uiElement.IsVisible)
                                        {
                                            return uiElement.IsHitTestVisible
                                                ? System.Windows.Media.HitTestFilterBehavior.Continue
                                                : System.Windows.Media.HitTestFilterBehavior.ContinueSkipSelf;
                                        }
                                    }
                                    return System.Windows.Media.HitTestFilterBehavior.ContinueSkipSelfAndChildren;
                                },
                            resultElement =>
                                {
                                    result = resultElement;
                                    return System.Windows.Media.HitTestResultBehavior.Stop;
                                },
                            new System.Windows.Media.PointHitTestParameters(localPoint));
                        if (result != null)
                        {
                            var hitDependencyObject = result.VisualHit;
                            return IsChildOf(frameworkElement, hitDependencyObject);
                        }

                        return false;
                    });
            }
        }

        private static System.Windows.FrameworkElement GetRootElement(System.Windows.FrameworkElement child)
        {
            System.Windows.FrameworkElement currentFrameworkElement = child;
            System.Windows.DependencyObject current = child;
            while (true)
            {
                var parent = System.Windows.Media.VisualTreeHelper.GetParent(current);
                if (parent == null)
                {
                    return currentFrameworkElement;
                }

                current = parent;
                var asFrameworkElement = current as System.Windows.FrameworkElement;
                if (asFrameworkElement != null)
                {
                    currentFrameworkElement = asFrameworkElement;
                }
            }
        }

        private static bool IsChildOf(System.Windows.DependencyObject parent, System.Windows.DependencyObject child)
        {
            System.Windows.DependencyObject current = child;
            while (true)
            {
                if (Equals(parent, current))
                {
                    return true;
                }

                current = System.Windows.Media.VisualTreeHelper.GetParent(current);
                if (current == null)
                {
                    return false;
                }
            }
        }

        public virtual MousePoint ClickablePoint
        {
            get
            {
                var bounds = this.BoundsOnScreen();
                var centerX = (int)(bounds.X + bounds.Width / 2);
                var centerY = (int)(bounds.Y + bounds.Height / 2);
                return new MousePoint(centerX, centerY);
            }
        }

        public void UpdateFoundBy(string foundByAsString)
        {
            _foundByAsString = foundByAsString;
        }

        public TTooltipElement Tooltip<TTooltipElement>()
            where TTooltipElement : class, ISearchSourceElement
        {
            return AllTooltips<TTooltipElement>().FirstOrDefault();
        }

        private IEnumerable<TTooltipElement> AllTooltips<TTooltipElement>()
            where TTooltipElement : class, ISearchSourceElement
        {
            System.Windows.Controls.ToolTip tooltip = OnUiThread.Get(this, frameworkElement =>
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
            return OnUiThread.Get(this, frameworkElement =>
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

        public TNativeElement GetStrongReference()
        {
            var strongReference = (TNativeElement)_frameworkElement.Target;
            if (strongReference != null)
            {
                return strongReference;
            }

            throw RuibarboException.NoLongerAvailable(this);
        }
    }

    public static class WpfElementExtensions
    {
        public static System.Windows.Rect BoundsOnScreen<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.FrameworkElement
        {
            return OnUiThread.Get(me, frameworkElement =>
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
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsHitTestVisible);
        }

        public static bool IsKeyboardFocused<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.FrameworkElement
        {
            return OnUiThread.Get(me, frameworkElement => frameworkElement.IsKeyboardFocused);
        }

        public static void BringIntoView<TNativeElement>(this WpfFrameworkElementBase<TNativeElement> me)
            where TNativeElement : System.Windows.FrameworkElement
        {
            OnUiThread.Invoke(me, frameworkElement => frameworkElement.BringIntoView());
            bool isInView = Wait.Until(() => me.IsInView());
            if (!isInView)
            {
                throw RuibarboException.StateFailed(me, x => x.IsInView());
            }
        }

        public static bool IsInView<TFrameworkElement>(this WpfFrameworkElementBase<TFrameworkElement> me)
            where TFrameworkElement : System.Windows.FrameworkElement
        {
            return OnUiThread.Get(me, element =>
                {
                    var container = System.Windows.Media.VisualTreeHelper.GetParent(element) as System.Windows.FrameworkElement;
                    if (container == null)
                    {
                        // Not really sure about this...
                        return true;
                    }

                    var elementBounds = element
                        .TransformToAncestor(container)
                        .TransformBounds(new System.Windows.Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
                    var containerBounds = new System.Windows.Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
                    var isInView = containerBounds.IntersectsWith(elementBounds);
                    //Console.WriteLine("IsInView '{0}' of {1}:{2} in '{3}' of {4}:{5}: {6} ({7} in {8})",
                    //    element.Name,
                    //    element.GetType().Name,
                    //    element.GetHashCode(),
                    //    container.Name,
                    //    container.GetType().Name,
                    //    container.GetHashCode(),
                    //    isInView,
                    //    elementBounds.ToString(),
                    //    containerBounds.ToString());
                    return isInView;
                });
        }
    }
}