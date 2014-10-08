using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using ruibarbo.core.Common;

namespace ruibarbo.core.Hardware
{
    public static class Keyboard
    {
        public static void Type(string value)
        {
            foreach (var ch in value)
            {
                SendInput(new[] { InputSimulator.CharDown(ch), InputSimulator.CharUp(ch), });
                Delay(Configuration.Instance.KeyboardDelayBetweenKeys);
            }

            Delay(Configuration.Instance.KeyboardDelayAfterTyping);
        }

        public static void TypeShortcut(params Key[] keys)
        {
            var downs = keys.Select(key => InputSimulator.KeyDown(KeyInterop.VirtualKeyFromKey(key)));
            var ups = keys.Reverse().Select(key => InputSimulator.KeyUp(KeyInterop.VirtualKeyFromKey(key)));
            var inputs = downs.Concat(ups).ToArray();
            SendInput(inputs);
        }

        private static void SendInput(InputSimulator.INPUT[] inputs)
        {
            var successful = InputSimulator.SendInput((UInt32) inputs.Length, inputs, Marshal.SizeOf(typeof (InputSimulator.INPUT)));
            if (successful != inputs.Length)
            {
                throw RuibarboException.HardwareFailure(Marshal.GetLastWin32Error());
            }
        }

        private static void Delay(TimeSpan keyboardDelayAfterTyping)
        {
            if (keyboardDelayAfterTyping > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(keyboardDelayAfterTyping);
            }
        }
    }
}