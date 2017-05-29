// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.

using SiliconStudio.Core;
using SiliconStudio.Core.Annotations;
using SiliconStudio.Xenko.Shaders;

namespace SiliconStudio.Xenko.Rendering.Materials
{
    /// <summary>
    /// Environment function for Schlick fresnel, Smith-Schlick GGX visibility and GGX normal distribution.
    /// </summary>
    /// <remarks>
    /// Based on https://knarkowicz.wordpress.com/2014/12/27/analytical-dfg-term-for-ibl/.
    /// Note: their glossiness-roughness conversion formula is not same as ours, this will need to be recomputed.
    /// </remarks>
    [DataContract("MaterialSpecularMicrofacetEnvironmentThinGlass")]
    [Display("Thin Glass")]
    public class MaterialSpecularMicrofacetEnvironmentThinGlass : IMaterialSpecularMicrofacetEnvironmentFunction
    {
        public ShaderSource Generate()
        {
            return new ShaderClassSource("MaterialSpecularMicrofacetEnvironmentThinGlass");
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is MaterialSpecularMicrofacetEnvironmentThinGlass;
        }

        public override int GetHashCode()
        {
            return typeof(MaterialSpecularMicrofacetEnvironmentThinGlass).GetHashCode();
        }
    }
}
