﻿using System;
using System.Windows;
using tungsten.core.Input;

namespace tungsten.core.Elements
{
    public class WpfTextBox : WpfElement<System.Windows.Controls.TextBox>
    {
        public WpfTextBox(SearchSourceElement searchParent, System.Windows.Controls.TextBox textBox)
            : base(searchParent, textBox)
        {
        }

        public void Type(string value)
        {
            if (!this.IsKeyboardFocused())
            {
                // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
                // TODO: Better error message. Include a lot of information about the control, including parents.
                // TODO: Better identification of FocusedElement (IInputElement)
                IInputElement focusedElement = Invoker.Get(() => System.Windows.Input.Keyboard.FocusedElement);
                throw new Exception("Can't type into TextBox since it does not have keyboard focus. Focus is in " + focusedElement);
            }

            Keyboard.Type(value);
        }
    }
}