using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using ruibarbo.core.Common;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Hardware;
using ruibarbo.core.Search;
using ruibarbo.core.Win32.Factory;
using ruibarbo.core.Win32.Native;

namespace ruibarbo.core.Win32
{
    [RegisteredControl]
    public class Win32Control : ISearchSourceElement, IAmFoundByUpdatable
    {
        private readonly ISearchSourceElement _searchParent;
        private string _foundByAsString;

        public Win32Control(ISearchSourceElement searchParent, IntPtr hwnd)
        {
            _searchParent = searchParent;
            Hwnd = hwnd;
            _foundByAsString = string.Empty;
        }

        internal IntPtr Hwnd { get; private set; }

        /// <summary>
        /// Returns the text of the title bar, the text of the control or an empty string. Returns null if it fails.
        /// </summary>
        public string Name
        {
            get { return Win32Api.GetWindowText(Hwnd); }
        }

        public string Class
        {
            get { return Win32Api.GetClassName(Hwnd); }
        }

        public virtual IEnumerable<object> NativeChildren
        {
            get { return Win32Api.GetChildWindows(Hwnd).Select(hwnd => new HwndWrapper(hwnd)); }
        }

        public object NativeParent
        {
            get { throw new NotImplementedException(); }
        }

        public string FoundBy
        {
            get { return _foundByAsString; }
        }

        public ISearchSourceElement SearchParent
        {
            get { return _searchParent; }
        }

        public int InstanceId
        {
            get { return Hwnd.ToInt32(); }
        }

        public bool IsVisible
        {
            get
            {
                var current = Hwnd;
                while (current != IntPtr.Zero)
                {
                    if (Win32Api.IsWindowVisible(current) == false)
                    {
                        return false;
                    }
                    current = Win32Api.GetParent(current);
                }
                return true;
            }
        }

        public bool IsEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Click()
        {
            Click(x => { });
        }

        public void Click(Action<Configurator> cfgAction)
        {
            var bounds = this.GetBoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY, cfgAction);
        }

        public void DoubleClick()
        {
            var bounds = this.GetBoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.DoubleClick(centerX, centerY);
        }

        public void UpdateFoundBy(string foundByAsString)
        {
            _foundByAsString = foundByAsString;
        }
    }

    public static class Win32ControlExtensions
    {
        public static Rect GetBoundsOnScreen(this Win32Control me)
        {
            return Win32Api.GetBoundsOnScreen(me.Hwnd);
        }
    }
}