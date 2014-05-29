using System;
using System.Windows;
using System.Windows.Threading;
using tungsten.core.Input;

namespace tungsten.core.Elements
{
    public class WpfTextBox : WpfElement
    {
        public WpfTextBox(Dispatcher dispatcher, SearchSourceElement parent, FrameworkElement frameworkElement)
            : base(dispatcher, parent, frameworkElement)
        {
        }

        public void Type(string value)
        {
            var strongReference = GetFrameworkElement();
            if (!GetDispatched(() => strongReference.IsKeyboardFocused))
            {
                // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
                // TODO: Better error message. Include a lot of information about the control, including parents.
                // TODO: Better identification of FocusedElement (IInputElement)
                IInputElement focusedElement = GetDispatched(() => System.Windows.Input.Keyboard.FocusedElement);
                throw new Exception("Can't type into TextBox since it does not have keyboard focus. Focus is in " + focusedElement);
            }

            Keyboard.Type(value);
        }
    }
}