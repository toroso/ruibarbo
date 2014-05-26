using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace tungsten.core
{
    public class WpfElement : IWpfElement
    {
        private readonly FrameworkElement _frameworkElement; // TODO: Weak reference
        private readonly Dispatcher _dispatcher;

        public WpfElement(FrameworkElement frameworkElement, Dispatcher dispatcher)
        {
            _frameworkElement = frameworkElement;
            _dispatcher = dispatcher;
        }

        public string Name
        {
            get { return GetDispatched(() => _frameworkElement.Name); }
        }

        public Type Class
        {
            get { return _frameworkElement.GetType(); }
        }

        public IEnumerable<WpfElement> Children
        {
            get
            {
                // TODO: Retry a few times if none is found
                var frameworkElementChildren = GetFrameworkElementChildren(_frameworkElement);
                return frameworkElementChildren.Select(x => new WpfElement(x, _dispatcher));
                // TODO: Factory that creates types
            }
        }

        private IEnumerable<FrameworkElement> GetFrameworkElementChildren(DependencyObject parent)
        {
            var result = new List<FrameworkElement>();

            int count = GetDispatched(() => VisualTreeHelper.GetChildrenCount(parent));
            for (int i = 0; i < count; i++)
            {
                var asDependencyObject = GetDispatched(() => VisualTreeHelper.GetChild(parent, i));
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

        public TRet GetDispatched<TRet>(Func<TRet> func)
        {
            TRet ret = default(TRet);
            _dispatcher.Invoke(() =>
            {
                ret = func();
            });
            return ret;
        }

        public void Click()
        {
            var locationFromWindow = GetDispatched(() => _frameworkElement.TranslatePoint(new Point(0.0, 0.0), null));
            var locationFromScreen = GetDispatched(() => _frameworkElement.PointToScreen(locationFromWindow));
            var width = GetDispatched(() => _frameworkElement.ActualWidth);
            var height = GetDispatched(() => _frameworkElement.ActualHeight);

            Console.WriteLine("Upper left: [{0}, {1}]", locationFromScreen.X, locationFromScreen.Y);
            Console.WriteLine("Size: [{0}, {1}]", width, height);

            //Move the mouse to the button position
            int x = (int) (locationFromScreen.X + width/2);
            int y = (int) (locationFromScreen.Y + height/2);

            Mouse.Click(x, y);
        }
    }
}