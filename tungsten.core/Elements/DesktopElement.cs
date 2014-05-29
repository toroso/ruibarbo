using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public class DesktopElement : SearchSourceElement
    {
        private readonly Application _application;

        public override string Name
        {
            get { return null; }
        }

        public override Type Class
        {
            get { return null; }
        }

        public override IEnumerable<By> SearchConditions
        {
            get { yield break; }
        }

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

        internal DesktopElement(Dispatcher dispatcher, Application application)
            : base(dispatcher, null)
        {
            _application = application;
        }
    }
}