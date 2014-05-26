using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace tungsten.core
{
    public class DesktopElement : SearchSourceElement
    {
        private readonly Application _application;

        public override IEnumerable<WpfElement> Children
        {
            get
            {
                var windows = GetDispatched(() => _application.Windows.Cast<Window>().ToArray());
                return windows
                    .Select(w => new WpfElement(w, Dispatcher)) // TODO: Factory that creates types
                    .ToArray();
            }
        }

        public DesktopElement(Application application, Dispatcher dispatcher)
            : base(dispatcher)
        {
            _application = application;
        }
    }
}