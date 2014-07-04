﻿using System;

namespace tungsten.core.Hardware
{
    public class HardwareConfigurator
    {
        /// <summary>
        /// When typing on the keyboard, how long should the delay be between each key press?
        /// The only reason for setting this to non-zero value is for demonstration: you will see what
        /// is typed on the keyboard.
        /// </summary>
        public TimeSpan KeyboardDelayBetweenKeys
        {
            set { HardwareConfiguration.KeyboardDelayBetweenKeys = value; }
        }

        /// <summary>
        /// How long should the delay be after typing a text string on the keyboard?
        /// </summary>
        public TimeSpan KeyboardDelayAfterTyping
        {
            set { HardwareConfiguration.KeyboardDelayAfterTyping = value; }
        }

        /// <summary>
        /// How long should the delay be after moving the mouse. This delay is for all mouse movements:
        /// just a simple move, a move to a mouse click and a drag-n-drop move.
        /// </summary>
        public TimeSpan MouseDelayAfterMove
        {
            set { HardwareConfiguration.MouseDelayAfterMove = value; }
        }

        /// <summary>
        /// How long should the delay be after clicking the mouse. This delay is the only one that is
        /// non-zero by default. Sometimes the tests are too fast for Windows, and, for instance, clicking
        /// a TextBox for focus requires a delay.
        /// </summary>
        public TimeSpan MouseDelayAfterClick
        {
            set { HardwareConfiguration.MouseDelayAfterClick = value; }
        }

        /// <summary>
        /// A duration of zero means all mouse movements are immediate. Setting this duration to non-zero,
        /// you will see the mouse slowely move from start to end point.
        /// </summary>
        public TimeSpan MouseDurationOfMove
        {
            set { HardwareConfiguration.MouseDurationOfMove = value; }
        }

        /// <summary>
        /// Should a screenshot be taken and saved to disk when an assertion fails?
        /// </summary>
        public bool ScreenshotOnFailedAssertion
        {
            set { HardwareConfiguration.ScreenshotOnFailedAssertion = value; }
        }
    }
}