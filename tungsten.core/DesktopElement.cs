using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace tungsten.core
{
    public class DesktopElement : IWpfElement
    {
        private readonly Application _application;
        private readonly Dispatcher _dispatcher;

        public string Name
        {
            get { return null; }
        }

        public Type Class
        {
            get { return null; }
        }

        public IEnumerable<WpfElement> Children
        {
            get
            {
                var windows = GetDispatched(() =>
                {
                    return _application.Windows.Cast<Window>().ToArray();
                });
                return windows
                    .Select(w => new WpfElement(w, _dispatcher)) // TODO: Factory that creates types
                    .ToArray();
            }
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
            throw new InvalidOperationException("Can't click desktop since it's outside of the application");
        }

        public DesktopElement(Application application, Dispatcher dispatcher)
        {
            _application = application;
            _dispatcher = dispatcher;
        }
    }
}