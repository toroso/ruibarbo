using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public class DesktopElement : SearchSourceElement
    {
        private readonly Application _application;

        internal DesktopElement(Application application)
            : base(null)
        {
            _application = application;
        }

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

        public override IEnumerable<UntypedWpfElement> Children
        {
            get
            {
                return FrameworkElementChildren
                    .SelectMany(CreateWpfElements)
                    .ToArray();
            }
        }

        public override IEnumerable<FrameworkElement> FrameworkElementChildren
        {
            get
            {
                // Application.Windows only returns Windows that are run on the same thread as Application. NonAppWindowsInternal
                // holds Windows run on other threads (non-documented, as an internal property).
                var windowsOnOtherThreads = (WindowCollection)_application
                    .GetType()
                    .GetProperty("NonAppWindowsInternal", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .GetValue(_application);
                var windows = windowsOnOtherThreads.Cast<Window>().ToArray();
                return windows;
            }
        }
    }
}