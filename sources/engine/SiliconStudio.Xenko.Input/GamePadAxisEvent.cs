// Copyright (c) 2016 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

namespace SiliconStudio.Xenko.Input
{
    /// <summary>
    /// An event to describe a change in a gamepad axis
    /// </summary>
    public class GamePadAxisEvent : AxisEvent
    {
        /// <summary>
        /// The gamepad axis identifier
        /// </summary>
        public GamePadAxis Axis;

        /// <summary>
        /// The gamepad that sent this event
        /// </summary>
        public IGamePadDevice GamePad => (IGamePadDevice)Device;

        public override string ToString()
        {
            return $"{nameof(Axis)}: {Axis}, {nameof(Value)}: {Value}, {nameof(GamePad)}: {GamePad.Name}";
        }
    }
}