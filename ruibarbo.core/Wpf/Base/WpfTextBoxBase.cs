using System;

using ruibarbo.core.Common;
using ruibarbo.core.Debug;
using ruibarbo.core.ElementFactory;
using ruibarbo.core.Wpf.Invoker;

namespace ruibarbo.core.Wpf.Base
{
    public class WpfTextBoxBase<TNativeElement> : WpfFrameworkElementBase<TNativeElement>
        where TNativeElement : System.Windows.Controls.TextBox
    {
        public WpfTextBoxBase(ISearchSourceElement searchParent, TNativeElement textBox)
            : base(searchParent, textBox)
        {
        }

        public void ClickAndSelectAll()
        {
            Click();
            TypeShortcut(System.Windows.Input.Key.LeftCtrl, System.Windows.Input.Key.A);
        }

        public string Text
        {
            get { return OnUiThread.Get(this, frameworkElement => frameworkElement.Text); }
        }

        public void Type(string value)
        {
            VerifyIsKeyboardFocused();
            Hardware.Keyboard.Type(value);
        }

        private void TypeShortcut(params System.Windows.Input.Key[] keys)
        {
            VerifyIsKeyboardFocused();
            Hardware.Keyboard.TypeShortcut(keys);
        }

        private void VerifyIsKeyboardFocused()
        {
            var isKeyboardFocused = Wait.Until(() => this.IsKeyboardFocused());
            if (!isKeyboardFocused)
            {
                var focusedElement = OnUiThread.Get(() => System.Windows.Input.Keyboard.FocusedElement);
                var focusedElementAsString = focusedElement != null
                    ? new DefaultControlToStringCreator().ControlToString(focusedElement)
                    : "<null>";
                string info = string.Format("Focused element is {0}", focusedElementAsString);
                throw RuibarboException.StateFailed(this, x => x.IsKeyboardFocused(), info);
            }
        }
    }

    public static class WpfTextBoxBaseExtensions
    {
        public static void ChangeTo<TNativeElement>(this WpfTextBoxBase<TNativeElement> me, string value)
            where TNativeElement : System.Windows.Controls.TextBox
        {
            me.ClickAndSelectAll();
            me.Type(value);
        }
    }
}