// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.

using System;

using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Engine;

namespace SiliconStudio.Xenko.Rendering.Lights
{
    /// <summary>
    /// A directional light.
    /// </summary>
    [DataContract("LightDirectional")]
    [Display("Directional")]
    public class LightDirectional : DirectLightBase
    {
        public LightDirectional()
        {
            Shadow = new LightDirectionalShadowMap
            {
                Size = LightShadowMapSize.Large
            };
        }

        public override bool HasBoundingBox
        {
            get
            {
                return false;
            }
        }

        public override BoundingBox ComputeBounds(Vector3 position, Vector3 direction)
        {
            return BoundingBox.Empty;
        }

        public override float ComputeScreenCoverage(RenderView renderView, Vector3 position, Vector3 direction)
        {
            // As the directional light is covering the whole screen, we take the max of current width, height
            return Math.Max(renderView.ViewSize.X, renderView.ViewSize.Y);
        }

        public override bool Update(LightComponent lightComponent)
        {
            return true;
        }
    }
}
