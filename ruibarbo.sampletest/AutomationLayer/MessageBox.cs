using System;
using System.Collections.Generic;
using System.Linq;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Search;
using ruibarbo.core.Win32;
using ruibarbo.core.Win32.Factory;

namespace ruibarbo.sampletest.AutomationLayer
{
    [RegisteredControl]
    public class MessageBox : Win32Control
    {
        public MessageBox(ISearchSourceElement searchParent, IntPtr hwnd)
            : base(searchParent, hwnd)
        {
        }

        public Win32Control OkButton
        {
            get
            {
                // TODO: Assert that number of buttons is 1 (OK) or 2 (OK/Cancel)
                return AllButtons.First();
            }
        }

        public Win32Control CancelButton
        {
            get
            {
                // TODO: Assert that number of buttons is 2 (OK/Cancel; Retry/Cancel) or 3 (Yes/No/Cancel) and choose corect index
                var win32Controls = AllButtons.ToArray();
                return win32Controls[1];
            }
        }

        public IEnumerable<Win32Control> AllButtons
        {
            get
            {
                return this
                    .FindAllChildren<Win32Control>(By.Class("Button"))
                    .OrderBy(ctrl => ctrl.GetBoundsOnScreen().Left);
            }
        }
    }
}