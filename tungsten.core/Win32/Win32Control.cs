using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using tungsten.core.Input;
using tungsten.core.Search;
using tungsten.core.Win32.Factory;

namespace tungsten.core.Win32
{
    public class Win32Control : ISearchSourceElement, IAmFoundByUpdatable
    {
        private readonly ISearchSourceElement _searchParent;
        private By[] _bys;

        public Win32Control(ISearchSourceElement searchParent, IntPtr hwnd)
        {
            _searchParent = searchParent;
            Hwnd = hwnd;
            _bys = new By[] { };
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

        public IEnumerable<By> FoundBys
        {
            get { return _bys; }
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
            get { throw new NotImplementedException(); }
        }

        public bool IsEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Click()
        {
            var bounds = this.GetBoundsOnScreen();
            var centerX = (int)(bounds.X + bounds.Width / 2);
            var centerY = (int)(bounds.Y + bounds.Height / 2);
            Mouse.Click(centerX, centerY);
        }

        public void FoundBy(IEnumerable<By> bys)
        {
            _bys = bys.AppendByClass(Class).ToArray();
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