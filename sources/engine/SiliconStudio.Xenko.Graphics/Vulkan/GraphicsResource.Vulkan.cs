// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
#if SILICONSTUDIO_XENKO_GRAPHICS_API_VULKAN
using System;
using SharpVulkan;

namespace SiliconStudio.Xenko.Graphics
{
    /// <summary>
    /// GraphicsResource class
    /// </summary>
    public abstract partial class GraphicsResource
    {
        internal DeviceMemory NativeMemory;
        internal long StagingFenceValue;

        protected bool IsDebugMode
        {
            get
            {
                return GraphicsDevice != null && GraphicsDevice.IsDebugMode;
            }
        }
    }
}
 
#endif
