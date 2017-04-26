// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;

namespace SiliconStudio.Assets.Analysis
{
    [Flags]
    public enum BuildDependencyType
    {
        /// <summary>
        /// This asset dependency will be necessary in a running game (could be preview and thumbnail too!)
        /// </summary>
        Runtime = 0x1,
        /// <summary>
        /// Only the asset is necessary
        /// </summary>
        CompileAsset = 0x2,
        /// <summary>
        /// The asset needs to be compiled because it will be Content.Load-ed by the command
        /// </summary>
        CompileContent = 0x4
    }
}
