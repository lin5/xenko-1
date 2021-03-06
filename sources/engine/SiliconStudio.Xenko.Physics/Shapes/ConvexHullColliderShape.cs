// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.

using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Graphics;
using System.Collections.Generic;
using System.Linq;
using SiliconStudio.Xenko.Extensions;
using SiliconStudio.Xenko.Graphics.GeometricPrimitives;
using SiliconStudio.Xenko.Rendering;

namespace SiliconStudio.Xenko.Physics
{
    public class ConvexHullColliderShape : ColliderShape
    {
        private readonly IReadOnlyList<Vector3> pointsList;
        private readonly IReadOnlyList<uint> indicesList; 

        public ConvexHullColliderShape(IReadOnlyList<Vector3> points, IReadOnlyList<uint> indices, Vector3 scaling)
        {
            Type = ColliderShapeTypes.ConvexHull;
            Is2D = false;

            CachedScaling = scaling;
            InternalShape = new BulletSharp.ConvexHullShape(points)
            {
                LocalScaling = CachedScaling
            };

            DebugPrimitiveMatrix = Matrix.Scaling(new Vector3(1, 1, 1) * DebugScaling);

            pointsList = points;
            indicesList = indices;
        }

        public IReadOnlyList<Vector3> Points
        {
            get { return pointsList; }
        }
        public IReadOnlyList<uint> Indices
        {
            get { return indicesList; }
        }

        public override MeshDraw CreateDebugPrimitive(GraphicsDevice device)
        {
            var verts = new VertexPositionNormalTexture[pointsList.Count];
            for (var i = 0; i < pointsList.Count; i++)
            {
                verts[i].Position = pointsList[i];
                verts[i].TextureCoordinate = Vector2.Zero;
                verts[i].Normal = Vector3.Zero;
            }

            var intIndices = indicesList.Select(x => (int)x).ToArray();

            ////calculate basic normals
            ////todo verify, winding order might be wrong?
            for (var i = 0; i < indicesList.Count; i += 3)
            {
                var i1 = intIndices[i];
                var i2 = intIndices[i + 1];
                var i3 = intIndices[i + 2];
                var a = verts[i1];
                var b = verts[i2];
                var c = verts[i3];
                var n = Vector3.Cross((b.Position - a.Position), (c.Position - a.Position));
                n.Normalize();
                verts[i1].Normal = verts[i2].Normal = verts[i3].Normal = n;
            }

            var meshData = new GeometricMeshData<VertexPositionNormalTexture>(verts, intIndices, false);

            return new GeometricPrimitive(device, meshData).ToMeshDraw();
        }
    }
}
