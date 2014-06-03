using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using tungsten.core.Input;
using tungsten.core.Utils;

namespace tungsten.core.Elements
{
    public abstract class WpfElement<TFrameworkElement> : UntypedWpfElement
        where TFrameworkElement : FrameworkElement
    {
        private readonly WeakReference<TFrameworkElement> _frameworkElement;

        protected WpfElement(SearchSourceElement parent, TFrameworkElement frameworkElement)
            : base(parent)
        {
            _frameworkElement = new WeakReference<TFrameworkElement>(frameworkElement);
        }

        public override string Name
        {
            get { return Get(frameworkElement => frameworkElement.Name); }
        }

        public override Type Class
        {
            get { return typeof (TFrameworkElement); }
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
            var centerPoint = Get(frameworkElement =>
                {
                    var locationFromScreen = frameworkElement.PointToScreen(new Point(0.0, 0.0));
                    var width = frameworkElement.ActualWidth;
                    var height = frameworkElement.ActualHeight;

                    var centerX = (int)(locationFromScreen.X + width / 2);
                    var centerY = (int)(locationFromScreen.Y + height / 2);

                    return new Point(centerX, centerY);
                });

            Mouse.Click((int) centerPoint.X, (int) centerPoint.Y);
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