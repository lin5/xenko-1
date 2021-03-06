// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using SiliconStudio.Assets.Serializers;
using SiliconStudio.Core;
using SiliconStudio.Core.Reflection;

namespace SiliconStudio.Xenko.Assets.Scripts
{
    [DataContract]
    [DataStyle(DataStyle.Compact)]
    public sealed class BlockReference : IAssetPartReference
    {
        /// <summary>
        /// Gets or sets the identifier of the asset part represented by this reference.
        /// </summary>
        public Guid Id { get; set; }

        [DataMemberIgnore]
        public Type InstanceType { get; set; }

        public void FillFromPart(object assetPart)
        {
            var block = (Block)assetPart;
            Id = block.Id;
        }

        public object GenerateProxyPart(Type partType)
        {
            var block = FakeBlock.Create();
            block.Id = Id;
            return block;
        }

        /// <summary>
        /// Used temporarily during deserialization when creating references.
        /// </summary>
        /// <remarks>
        /// We don't expose a public ctor so that is not listed in list of available blocks to create.
        /// </remarks>
        class FakeBlock : Block
        {
            private FakeBlock()
            {
            }

            internal static FakeBlock Create()
            {
                return new FakeBlock();
            }

            public override void GenerateSlots(IList<Slot> newSlots, SlotGeneratorContext context)
            {
            }
        }
    }
}
