using tungsten.core.Search;
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
            TypeShortcut(System.Windows.Input.Key.LeftCtrl, System.Windows.Input.Key.A);
        }

        public string Text
        {
            get { return Invoker.Get(this, frameworkElement => frameworkElement.Text); }
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
            var isKeyboardFocused = Wait.Until(this.IsKeyboardFocused);
            if (!isKeyboardFocused)
            {
                var focusedElement = Invoker.Get(() => System.Windows.Input.Keyboard.FocusedElement);
                var focusedElementAsString = focusedElement != null
                    ? new DefaultControlToStringCreator().ControlToString(focusedElement)
                    : "<null>";
                string info = string.Format("Focused element is {0}", focusedElementAsString);
                throw ManglaException.StateFailed(this, x => x.IsKeyboardFocused(), info);
            }
        }
    }
}