using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace tungsten.core.Input
{
    public static class Keyboard
    {
        public static void Type(string value)
        {
            // TODO: Using some kind of configuration, insert delays so that it's possible to see the keys. Send one by one?
            var inputs = value
                .SelectMany(ch => new[]
                    {
                        InputSimulator.CharDown(ch),
                        InputSimulator.CharUp(ch),
                    })
                .ToArray();
            SendInput(inputs);

            var keyboardDelayAfterTyping = HardwareConfiguration.KeyboardDelayAfterTyping;
            if (keyboardDelayAfterTyping > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(keyboardDelayAfterTyping);
            }
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
                throw ManglaException.HardwareFailure(Marshal.GetLastWin32Error());
            }
        }
    }
}