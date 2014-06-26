using System;
using System.Windows.Input;

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
            Input.Keyboard.TypeShortcut(Key.LeftCtrl, Key.A);
        }

        public string Text
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Text); }
        }

        public void Type(string value)
        {
            if (!this.IsKeyboardFocused())
            {
                // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
                // TODO: Better error message. Include a lot of information about the control, including parents.
                // TODO: Better identification of FocusedElement (IInputElement)
                var focusedElementAsString = Invoker.Get(() => Keyboard.FocusedElement.ToString());
                throw new Exception("Can't type into TextBox since it does not have keyboard focus. Focus is in " + focusedElementAsString);
            }

            Input.Keyboard.Type(value);
        }
    }
}