using System;
using System.Runtime.InteropServices;
using ruibarbo.core.Common;

namespace ruibarbo.core.Hardware
{
    public static class Mouse
    {
        public static void Click(IClickable clickable)
        {
            var point = clickable.ClickablePoint;
            Click(point.X, point.Y);
        }

        public static void Click(int x, int y)
        {
            // TODO: Use Configuration.MouseDurationOfMove
            MoveCursor(x, y);
            var mouseDelayAfterMove = Configuration.MouseDelayAfterMove;
            if (mouseDelayAfterMove > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(mouseDelayAfterMove);
            }

            ClickLeftButton();
            var mouseDelayAfterClick = Configuration.MouseDelayAfterClick;
            if (mouseDelayAfterClick > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(mouseDelayAfterClick);
            }
        }

        public static void MoveCursor(IClickable clickable)
        {
            var point = clickable.ClickablePoint;
            MoveCursor(point.X, point.Y);
        }

        public static void MoveCursor(int x, int y)
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

    public interface IClickable
    {
        MousePoint ClickablePoint { get; }
    }

    public class MousePoint
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public MousePoint(int x, int y)
        {
            Y = y;
            X = x;
        }
    }
}