// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.

using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;

namespace SiliconStudio.Xenko.Rendering.Materials
{
    public class MaterialComputeColorKeys
    {
        public MaterialComputeColorKeys(ObjectParameterKey<Texture> textureBaseKey, ParameterKey valueBaseKey, Color? defaultTextureValue = null, bool isColor = true)
        {
            //if (textureBaseKey == null) throw new ArgumentNullException("textureBaseKey");
            //if (valueBaseKey == null) throw new ArgumentNullException("valueBaseKey");
            TextureBaseKey = textureBaseKey;
            ValueBaseKey = valueBaseKey;
            DefaultTextureValue = defaultTextureValue;
            IsColor = isColor;
        }

        public ObjectParameterKey<Texture> TextureBaseKey { get; private set; }

        public ParameterKey ValueBaseKey { get; private set; }

        public Color? DefaultTextureValue { get; private set; }

        public bool IsColor { get; private set; }
    }
}
