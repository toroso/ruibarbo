using System;
using System.Threading;

namespace ruibarbo.core.Common
{
    public class Configuration
    {
        private static readonly ThreadLocal<Configuration> Instances = new ThreadLocal<Configuration>();

        internal static Configuration Instance
        {
            get
            {
                if (!Instances.IsValueCreated)
                {
                    Instances.Value = new Configuration();
                }
                return Instances.Value;
            }
        }

        public TimeSpan MaxRetryTime { get; set; }

        public TimeSpan KeyboardDelayBetweenKeys { get; set; }

        public TimeSpan KeyboardDelayAfterTyping { get; set; }

        public TimeSpan MouseDelayAfterMove { get; set; }

        public TimeSpan MouseDelayAfterClick { get; set; }

        public TimeSpan MouseDelayBetweenDownAndUp { get; set; }

        public TimeSpan MouseDurationOfMove { get; set; }

        public TimeSpan DelayWhenOpeningComboBox { get; set; }

        public bool ScreenshotOnFailedAssertion { get; set; }

        public Configuration Clone()
        {
            return new Configuration
                {
                    MaxRetryTime = MaxRetryTime,
                    KeyboardDelayBetweenKeys = KeyboardDelayBetweenKeys,
                    KeyboardDelayAfterTyping = KeyboardDelayAfterTyping,
                    MouseDelayAfterMove = MouseDelayAfterMove,
                    MouseDelayAfterClick = MouseDelayAfterClick,
                    MouseDelayBetweenDownAndUp = MouseDelayBetweenDownAndUp,
                    MouseDurationOfMove = MouseDurationOfMove,
                    DelayWhenOpeningComboBox = DelayWhenOpeningComboBox,
                    ScreenshotOnFailedAssertion = ScreenshotOnFailedAssertion,
                };
        }
    }
}