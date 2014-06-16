using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using tungsten.core.Search;

namespace tungsten.core.Elements
{
    public class DesktopElement : ISearchSourceElement
    {
        private readonly Application _application;

        internal DesktopElement(Application application)
        {
            _application = application;
        }

        public virtual string Name
        {
            get { return null; }
        }

        public virtual Type Class
        {
            get { return null; }
        }

        public virtual IEnumerable<FrameworkElement> NativeChildren
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

        public FrameworkElement NativeParent
        {
            get { return null; }
        }

        public virtual IEnumerable<By> FoundBys
        {
            get { yield break; }
        }

        public virtual ISearchSourceElement SearchParent
        {
            get { return null; }
        }

        public virtual int InstanceId
        {
            get { return _application.GetHashCode(); }
        }
    }
}