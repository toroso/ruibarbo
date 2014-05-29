using System;
using System.Linq;
using System.Runtime.InteropServices;

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
                        InputSimulator.KeyDown(ch),
                        InputSimulator.KeyUp(ch),
                    })
                .ToArray();
            var successful = InputSimulator.SendInput((UInt32)inputs.Length, inputs, Marshal.SizeOf(typeof(InputSimulator.INPUT)));
            if (successful != inputs.Length)
            {
                // TODO: Inject IAssertionExceptionFactory that can create NUnit, MSTest or whatever assertion exceptions
                // Can I get LastError?
                throw new Exception("Some simulated input commands were not sent successfully.");
            }

            var keyboardDelayAfterTyping = HardwareConfiguration.KeyboardDelayAfterTyping;
            if (keyboardDelayAfterTyping > TimeSpan.Zero)
            {
                System.Threading.Thread.Sleep(keyboardDelayAfterTyping);
            }
        }
    }
}