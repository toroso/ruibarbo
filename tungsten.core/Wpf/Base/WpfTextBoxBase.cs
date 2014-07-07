using System.Windows.Input;
using tungsten.core.Utils;

namespace tungsten.core.Wpf.Base
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
            Hardware.Keyboard.TypeShortcut(Key.LeftCtrl, Key.A);
        }

        public string Text
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Text); }
        }

        public void Type(string value)
        {
            if (!this.IsKeyboardFocused())
            {
                var focusedElement = Invoker.Get(() => Keyboard.FocusedElement);
                var focusedElementAsString = focusedElement != null
                    ? new DefaultControlToStringCreator().ControlToString(focusedElement)
                    : "<null>";
                string info = string.Format("Focused element is {0}", focusedElementAsString);
                throw ManglaException.StateFailed(this, x => x.IsKeyboardFocused(), info);
            }

            Hardware.Keyboard.Type(value);
        }
    }
}