using System;
using System.Runtime.InteropServices;

namespace tungsten.core.Input
{
    public static class Mouse
    {
        public static void Click(int x, int y)
        {
            // TODO: Use HardwareConfiguration.MouseDurationOfMove
            MoveCursor(x, y);
            var mouseDelayAfterMove = HardwareConfiguration.MouseDelayAfterMove;
            if (mouseDelayAfterMove > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(mouseDelayAfterMove);
            }

            ClickLeftButton();
            var mouseDelayAfterClick = HardwareConfiguration.MouseDelayAfterClick;
            if (mouseDelayAfterClick > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(mouseDelayAfterClick);
            }
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
                throw ManglaException.HardwareFailure(Marshal.GetLastWin32Error());
            }
        }
    }
}