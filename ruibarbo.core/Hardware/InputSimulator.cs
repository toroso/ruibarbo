using System;
using System.Runtime.InteropServices;

namespace ruibarbo.core.Hardware
{
    internal static class InputSimulator
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 SendInput(UInt32 numberOfInputs, INPUT[] inputs, Int32 sizeOfInputStructure);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [Flags]
        internal enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        /// <summary>
        /// The event type contained in the union field
        /// </summary>
        internal enum SendInputEventType : int
        {
            /// <summary>
            /// Contains Mouse event data
            /// </summary>
            InputMouse,
            /// <summary>
            /// Contains Keyboard event data
            /// </summary>
            InputKeyboard,
            /// <summary>
            /// Contains Hardware event data
            /// </summary>
            InputHardware
        }

        /// <summary>
        /// The mouse data structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseInputData
        {
            /// <summary>
            /// The x value, if ABSOLUTE is passed in the flag then this is an actual X and Y value
            /// otherwise it is a delta from the last position
            /// </summary>
            public int dx;
            /// <summary>
            /// The y value, if ABSOLUTE is passed in the flag then this is an actual X and Y value
            /// otherwise it is a delta from the last position
            /// </summary>
            public int dy;
            /// <summary>
            /// Wheel event data, X buttons
            /// </summary>
            public uint mouseData;
            /// <summary>
            /// ORable field with the various flags about buttons and nature of event
            /// </summary>
            public MouseEventFlags dwFlags;
            /// <summary>
            /// The timestamp for the event, if zero then the system will provide
            /// </summary>
            public uint time;
            /// <summary>
            /// Additional data obtained by calling app via GetMessageExtraInfo
            /// </summary>
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        /// <summary>
        /// Captures the union of the three three structures.
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        internal struct MouseKeybdhardwareInputUnion
        {
            /// <summary>
            /// The Mouse Input Data
            /// </summary>
            [FieldOffset(0)]
            public MouseInputData mi;

            /// <summary>
            /// The Keyboard input data
            /// </summary>
            [FieldOffset(0)]
            public KEYBDINPUT ki;

            /// <summary>
            /// The hardware input data
            /// </summary>
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        /// <summary>
        /// The Data passed to SendInput in an array.
        /// </summary>
        /// <remarks>Contains a union field type specifies what it contains </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            /// <summary>
            /// The actual data type contained in the union Field
            /// </summary>
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }

        /// <summary>
        /// Specifies various aspects of a keystroke. This member can be certain combinations of the following values.
        /// </summary>
        [Flags]
        internal enum KeyboardFlag : uint // UInt32
        {
            /// <summary>
            /// KEYEVENTF_EXTENDEDKEY = 0x0001 (If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).)
            /// </summary>
            ExtendedKey = 0x0001,

            /// <summary>
            /// KEYEVENTF_KEYUP = 0x0002 (If specified, the key is being released. If not specified, the key is being pressed.)
            /// </summary>
            KeyUp = 0x0002,

            /// <summary>
            /// KEYEVENTF_UNICODE = 0x0004 (If specified, wScan identifies the key and wVk is ignored.)
            /// </summary>
            Unicode = 0x0004,

            /// <summary>
            /// KEYEVENTF_SCANCODE = 0x0008 (Windows 2000/XP: If specified, the system synthesizes a VK_PACKET keystroke. The wVk parameter must be zero. This flag can only be combined with the KEYEVENTF_KEYUP flag. For more information, see the Remarks section.)
            /// </summary>
            ScanCode = 0x0008,
        }

        public static INPUT CharDown(char ch)
        {
            return Char(ch, KeyboardFlag.Unicode);
        }

        public static INPUT CharUp(char ch)
        {
            return Char(ch, KeyboardFlag.Unicode | KeyboardFlag.KeyUp);
        }

        private static INPUT Char(char ch, KeyboardFlag flags)
        {
            return new INPUT
                {
                    type = SendInputEventType.InputKeyboard,
                    mkhi = new MouseKeybdhardwareInputUnion
                        {
                            ki = new KEYBDINPUT
                                {
                                    wVk = 0,
                                    wScan = ch,
                                    dwFlags = (uint)flags,
                                    time = 0,
                                    dwExtraInfo = IntPtr.Zero,
                                }
                        }
                };
        }

        public static INPUT KeyDown(int virtualKey)
        {
            return Key(virtualKey, 0);
        }

        public static INPUT KeyUp(int virtualKey)
        {
            return Key(virtualKey, KeyboardFlag.KeyUp);
        }

        private static INPUT Key(int virtualKey, KeyboardFlag flags)
        {
            var allFlags = (virtualKey & 0xFF00) == 0xE000
                ? flags | KeyboardFlag.ExtendedKey
                : flags;

            return new INPUT
                {
                    type = SendInputEventType.InputKeyboard,
                    mkhi = new MouseKeybdhardwareInputUnion
                        {
                            ki = new KEYBDINPUT
                                {
                                    wVk = (ushort)virtualKey,
                                    wScan = 0,
                                    dwFlags = (uint)allFlags,
                                    time = 0,
                                    dwExtraInfo = IntPtr.Zero,
                                }
                        }
                };
        }

        public static INPUT LeftMouseButtonDown()
        {
            return MouseButtonEvent(MouseEventFlags.LEFTDOWN);
        }

        public static INPUT LeftMouseButtonUp()
        {
            return MouseButtonEvent(MouseEventFlags.LEFTUP);
        }

        private static INPUT MouseButtonEvent(MouseEventFlags flags)
        {
            return new INPUT
                {
                    type = SendInputEventType.InputMouse,
                    mkhi = new MouseKeybdhardwareInputUnion
                        {
                            mi = new MouseInputData
                                {
                                    dwFlags = flags,
                                }
                        }
                };
        }
    }
}