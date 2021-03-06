// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.

using System;

namespace SiliconStudio.Xenko.Rendering.LightProbes
{
    public struct TetrahedronSortKey : IComparable<TetrahedronSortKey>
    {
        public int Index;
        public int SortKey;

        public TetrahedronSortKey(int index, int sortKey)
        {
            Index = index;
            SortKey = sortKey;
        }

        public int CompareTo(TetrahedronSortKey other)
        {
            return SortKey.CompareTo(other.SortKey);
        }

        public override string ToString()
        {
            return $"Tetrahedron Index: {Index}; SortKey: {SortKey}";
        }
    }
}
