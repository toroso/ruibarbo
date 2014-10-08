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
            Delay(Configuration.MouseDelayAfterMove);

            ClickLeftButton();
            Delay(Configuration.MouseDelayAfterClick);
        }

        public static void DoubleClick(IClickable clickable)
        {
            var point = clickable.ClickablePoint;
            DoubleClick(point.X, point.Y);
        }

        public static void DoubleClick(int x, int y)
        {
            // TODO: Use Configuration.MouseDurationOfMove
            MoveCursor(x, y);
            Delay(Configuration.MouseDelayAfterMove);

            ClickLeftButton();
            Delay(Configuration.MouseDelayBetweenDownAndUp);
            ClickLeftButton();
            Delay(Configuration.MouseDelayAfterClick);
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
            LeftButtonDown();
            Delay(Configuration.MouseDelayBetweenDownAndUp);
            LeftButtonUp();
        }

        private static void LeftButtonDown()
        {
            SendInput(new[] { InputSimulator.LeftMouseButtonDown() });
        }

        private static void LeftButtonUp()
        {
            SendInput(new[] { InputSimulator.LeftMouseButtonUp(), });
        }

        private static void SendInput(InputSimulator.INPUT[] inputs)
        {
            var successful = InputSimulator.SendInput((UInt32)inputs.Length, inputs, Marshal.SizeOf(typeof(InputSimulator.INPUT)));
            if (successful != inputs.Length)
            {
                throw RuibarboException.HardwareFailure(Marshal.GetLastWin32Error());
            }
        }

        private static void Delay(TimeSpan mouseDelayAfterMove)
        {
            if (mouseDelayAfterMove > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(mouseDelayAfterMove);
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

        public override string ToString()
        {
            return string.Format("{0};{1}", X, Y);
        }
    }
}