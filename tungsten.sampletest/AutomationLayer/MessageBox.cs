using System;
using tungsten.core;
using tungsten.core.Search;
using tungsten.core.Win32;

namespace tungsten.sampletest.AutomationLayer
{
    public class MessageBox : Win32Control
    {
        public MessageBox(ISearchSourceElement searchParent, IntPtr hwnd)
            : base(searchParent, hwnd)
        {
        }

        public Win32Control OkButton
        {
            get { return this.FindFirstChild<Win32Control>(By.Class("Button")); }
        }
    }
}