using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using tungsten.core.ElementFactory;

namespace tungsten.core.Win32.Factory
{
    public class Win32ControlFactory : IElementFactory
    {
        private readonly List<Type> _types = new List<Type>();

        public Win32ControlFactory(Action<IWin32FactoryConfigurator> configAction)
        {
            configAction(new Win32FactoryConfigurator(this));
        }

        public IEnumerable<ISearchSourceElement> CreateElements(ISearchSourceElement parent, object nativeObject)
        {
            var hwndWrapper = nativeObject as HwndWrapper;
            if (hwndWrapper != null)
            {
                return _types.Select(win32Type => (ISearchSourceElement) Activator.CreateInstance(win32Type, parent, hwndWrapper.Hwnd));
            }

            return new ISearchSourceElement[] { };
        }

        public IEnumerable<object> GetRootElements()
        {
            int processId = Process.GetCurrentProcess().Id;
            return Win32Api.GetProcessWindows(processId)
                .Select(hwnd => new HwndWrapper(hwnd));
        }

        public void AddControl<TWin32Control>()
            where TWin32Control : Win32Control
        {
            _types.Add(typeof(TWin32Control));
        }
    }
}