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
                var focusedElement = Invoker.Get(() => Keyboard.FocusedElement);
                throw ManglaException.NotFocused(this, focusedElement);
            }

            Input.Keyboard.Type(value);
        }
    }
}