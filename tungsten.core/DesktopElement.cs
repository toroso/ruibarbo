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
                    .Select(CreateWpfElement)
                    .ToArray();
            }
        }

        internal DesktopElement(Dispatcher dispatcher, ElementFactory.ElementFactory elementFactory, Application application)
            : base(dispatcher, elementFactory)
        {
            _application = application;
        }
    }
}