using System;

namespace ruibarbo.core.Common
{
    public sealed class Configurator
    {
        /// <summary>
        /// How long should we wait for a condition to become true? This duration is used when finding elements and when waiting
        /// for a state to become true, both internally and in explicitly stated assertions.
        /// </summary>
        public TimeSpan MaxRetryTime
        {
            set { Configuration.MaxRetryTime = value; }
        }

        /// <summary>
        /// How long should we wait for the ComboBox popup to open after clicking it.
        /// On fast computers, this can be set to zero. On slow computers you might need up to 100 ms.
        /// </summary>
        public TimeSpan DelayWhenOpeningComboBox
        {
            set { Configuration.DelayWhenOpeningComboBox = value; }
        }

        /// <summary>
        /// When typing on the keyboard, how long should the delay be between each key press?
        /// The only reason for setting this to non-zero value is for demonstration: you will see what
        /// is typed on the keyboard.
        /// </summary>
        public TimeSpan KeyboardDelayBetweenKeys
        {
            set { Configuration.KeyboardDelayBetweenKeys = value; }
        }

        /// <summary>
        /// How long should the delay be after typing a text string on the keyboard?
        /// </summary>
        public TimeSpan KeyboardDelayAfterTyping
        {
            set { Configuration.KeyboardDelayAfterTyping = value; }
        }

        /// <summary>
        /// How long should the delay be after moving the mouse. This delay is for all mouse movements:
        /// just a simple move, a move to a mouse click and a drag-n-drop move.
        /// </summary>
        public TimeSpan MouseDelayAfterMove
        {
            set { Configuration.MouseDelayAfterMove = value; }
        }

        /// <summary>
        /// How long should the delay be after clicking the mouse. This delay is the only one that is
        /// non-zero by default. Sometimes the tests are too fast for Windows, and, for instance, clicking
        /// a TextBox for focus requires a delay.
        /// </summary>
        public TimeSpan MouseDelayAfterClick
        {
            set { Configuration.MouseDelayAfterClick = value; }
        }

        /// <summary>
        /// A duration of zero means all mouse movements are immediate. Setting this duration to non-zero,
        /// you will see the mouse slowely move from start to end point.
        /// </summary>
        public TimeSpan MouseDurationOfMove
        {
            set { Configuration.MouseDurationOfMove = value; }
        }

        /// <summary>
        /// Should a screenshot be taken and saved to disk when an assertion fails?
        /// </summary>
        public bool ScreenshotOnFailedAssertion
        {
            set { Configuration.ScreenshotOnFailedAssertion = value; }
        }
    }
}