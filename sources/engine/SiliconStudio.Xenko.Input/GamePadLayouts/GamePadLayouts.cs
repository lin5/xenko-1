﻿// Copyright (c) 2016 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using System.Collections.Generic;

namespace SiliconStudio.Xenko.Input
{
    /// <summary>
    /// Keeps track of <see cref="GamePadLayout"/>s
    /// </summary>
    public static class GamePadLayouts
    {
        private static List<GamePadLayout> layouts = new List<GamePadLayout>();

        static GamePadLayouts()
        {
            // Register layouts from the mapping database
            GameControllerDb.RegisterLayouts();
        }

        /// <summary>
        /// Adds a new layout that cane be used for mapping gamepads to <see cref="GamePadState"/>s
        /// </summary>
        /// <param name="layout">The layout to add</param>
        public static void AddLayout(GamePadLayout layout)
        {
            if (!layouts.Contains(layout))
            {
                layouts.Add(layout);
            }
        }

        /// <summary>
        /// Finds a layout matching the given gamepad
        /// </summary>
        /// <param name="source">The source that the <paramref name="device"/> came from</param>
        /// <param name="device">The device to find a layout for</param>
        /// <returns>The gamepad layout that was found, or null if none was found</returns>
        public static GamePadLayout FindLayout(IInputSource source, IGameControllerDevice device)
        {
            foreach (var layout in layouts)
            {
                if (layout.MatchDevice(source, device))
                    return layout;
            }
            return null;
        }
    }
}