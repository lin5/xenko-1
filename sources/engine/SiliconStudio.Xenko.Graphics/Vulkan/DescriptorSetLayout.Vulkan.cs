﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using System;
using System.Collections.Generic;
using SiliconStudio.Core;
using SiliconStudio.Core.Collections;
using SiliconStudio.Xenko.Shaders;
#if SILICONSTUDIO_XENKO_GRAPHICS_API_VULKAN
using SharpVulkan;

namespace SiliconStudio.Xenko.Graphics
{
    public partial class DescriptorSetLayout
    {
        internal readonly DescriptorSetLayoutBuilder Builder;

        internal SharpVulkan.DescriptorSetLayout NativeLayout;
        internal Sampler[] ImmutableSamplers;

        private DescriptorSetLayout(GraphicsDevice device, DescriptorSetLayoutBuilder builder) : base(device)
        {
            this.Builder = builder;
            Recreate();
        }

        private void Recreate()
        {
            NativeLayout = CreateNativeDescriptorSetLayout(GraphicsDevice, Builder.Entries, out ImmutableSamplers);
        }

        internal static unsafe SharpVulkan.DescriptorSetLayout CreateNativeDescriptorSetLayout(GraphicsDevice device, IList<DescriptorSetLayoutBuilder.Entry> entries, out Sampler[] immutableSamplers)
        {
            var bindings = new DescriptorSetLayoutBinding[entries.Count];
            immutableSamplers = new Sampler[entries.Count];

            int usedBindingCount = 0;

            fixed (Sampler* immutableSamplersPointer = &immutableSamplers[0])
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    var entry = entries[i];
                    if (!entry.IsUsed)
                        continue;

                    bindings[usedBindingCount] = new DescriptorSetLayoutBinding
                    {
                        DescriptorType = VulkanConvertExtensions.ConvertDescriptorType(entry.Class),
                        StageFlags = ShaderStageFlags.All, // TODO VULKAN: Filter?
                        Binding = (uint)i,
                        DescriptorCount = (uint)entry.ArraySize
                    };

                    if (entry.ImmutableSampler != null)
                    {
                        // TODO VULKAN: Handle immutable samplers for DescriptorCount > 1
                        if (entry.ArraySize > 1)
                        {
                            throw new NotImplementedException();
                        }

                        // Remember this, so we can choose the right DescriptorType in DescriptorSet.SetShaderResourceView
                        immutableSamplers[i] = entry.ImmutableSampler.NativeSampler;
                        //bindings[i].DescriptorType = DescriptorType.CombinedImageSampler;
                        bindings[usedBindingCount].ImmutableSamplers = new IntPtr(immutableSamplersPointer + i);
                    }

                    usedBindingCount++;
                }

                var createInfo = new DescriptorSetLayoutCreateInfo
                {
                    StructureType = StructureType.DescriptorSetLayoutCreateInfo,
                    BindingCount = (uint)usedBindingCount,
                    Bindings = usedBindingCount > 0 ? new IntPtr(Interop.Fixed(bindings)) : IntPtr.Zero
                };
                return device.NativeDevice.CreateDescriptorSetLayout(ref createInfo);
            }
        }

        /// <inheritdoc/>
        protected internal override bool OnRecreate()
        {
            Recreate();
            return true;
        }

        /// <inheritdoc/>
        protected internal override unsafe void OnDestroyed()
        {
            GraphicsDevice.NativeDevice.DestroyDescriptorSetLayout(NativeLayout);
            NativeLayout = SharpVulkan.DescriptorSetLayout.Null;

            base.OnDestroyed();
        }
    }
}
#endif