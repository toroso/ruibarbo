using System;
using System.Runtime.InteropServices;

namespace tungsten.core.Input
{
    public static class Mouse
    {
        public static void Click(int x, int y)
        {
            // TODO: Using some kind of configuration, insert delays so that it's possible to see the movements
            MoveCursor(x, y);
            System.Threading.Thread.Sleep(10);
            //System.Threading.Thread.Sleep(1000);
            ClickLeftButton();
            System.Threading.Thread.Sleep(10);
        }

        private static void MoveCursor(int x, int y)
        {
            InputSimulator.SetCursorPos(x, y);
        }

        private static void ClickLeftButton()
        {
            var inputs = new[]
                {
                    InputSimulator.LeftMouseButtonDown(),
                    InputSimulator.LeftMouseButtonUp()
                };

            var successful = InputSimulator.SendInput((UInt32)inputs.Length, inputs, Marshal.SizeOf(typeof(InputSimulator.INPUT)));
            if (successful != inputs.Length)
            {
                // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
                // Can I get LastError?
                throw new Exception("Some simulated input commands were not sent successfully.");
            }
        }
    }
}