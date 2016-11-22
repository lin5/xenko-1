﻿// Copyright (c) 2016 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

#if SILICONSTUDIO_XENKO_UI_SDL
using System;

namespace SiliconStudio.Xenko.Input
{
    /// <summary>
    /// A known gamepad that uses SDL as a driver
    /// </summary>
    public class GamePadSDL : GamePadFromLayout, IGamePadIndexAssignable
    {
        public GamePadSDL(InputManager inputManager, GameControllerSDL controller, GamePadLayout layout)
            : base(inputManager, controller, layout)
        {
            Name = controller.Name;
            Id = controller.Id;
            ProductId = controller.ProductId;
        }

        public new int Index
        {
            get { return base.Index; }
            set { SetIndexInternal(value, false); }
        }

        public override string Name { get; }
        public override Guid Id { get; }
        public override Guid ProductId { get; }

        public override void SetVibration(float smallLeft, float smallRight, float largeLeft, float largeRight)
        {
            // No vibration support in SDL gamepads
        }
    }
}
#endif