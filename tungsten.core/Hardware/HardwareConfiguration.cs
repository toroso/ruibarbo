using System;
using System.Threading;

namespace tungsten.core.Hardware
{
    public class HardwareConfiguration
    {
        private static readonly ThreadLocal<HardwareConfiguration> Instances = new ThreadLocal<HardwareConfiguration>();

        private static HardwareConfiguration Instance
        {
            get
            {
                if (!Instances.IsValueCreated)
                {
                    Instances.Value = new HardwareConfiguration();
                }
                return Instances.Value;
            }
        }

        private TimeSpan _keyboardDelayBetweenKeys;

        public static TimeSpan KeyboardDelayBetweenKeys
        {
            get { return Instance._keyboardDelayBetweenKeys; }
            set { Instance._keyboardDelayBetweenKeys = value; }
        }

        private TimeSpan _keyboardDelayAfterTyping;

        public static TimeSpan KeyboardDelayAfterTyping
        {
            get { return Instance._keyboardDelayAfterTyping; }
            set { Instance._keyboardDelayAfterTyping = value; }
        }

        private TimeSpan _mouseDelayAfterMove;

        public static TimeSpan MouseDelayAfterMove
        {
            get { return Instance._mouseDelayAfterMove; }
            set { Instance._mouseDelayAfterMove = value; }
        }

        private TimeSpan _mouseDelayAfterClick;

        public static TimeSpan MouseDelayAfterClick
        {
            get { return Instance._mouseDelayAfterClick; }
            set { Instance._mouseDelayAfterClick = value; }
        }

        private TimeSpan _mouseDurationOfMove;

        public static TimeSpan MouseDurationOfMove
        {
            get { return Instance._mouseDurationOfMove; }
            set { Instance._mouseDurationOfMove = value; }
        }

        private bool _screenshotOnFailedAssertion;

        public static bool ScreenshotOnFailedAssertion
        {
            get { return Instance._screenshotOnFailedAssertion; }
            set { Instance._screenshotOnFailedAssertion = value; }
        }
    }
}