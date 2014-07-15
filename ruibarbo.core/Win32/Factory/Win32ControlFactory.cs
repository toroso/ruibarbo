using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ruibarbo.core.ElementFactory;

namespace ruibarbo.core.Win32.Factory
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
            return Win32Api.GetProcessWindows()
                .Where(hwnd => !Win32Api.GetClassName(hwnd).StartsWith("HwndWrapper"))
                .Select(hwnd => new HwndWrapper(hwnd));
        }

        public void AddRegisteredElementsInAssembly(Assembly assembly)
        {
            var registeredElementTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(RegisteredControlAttribute), true).Any());
            foreach (var elementType in registeredElementTypes)
            {
                _types.Add(elementType);
            }
        }
    }
}