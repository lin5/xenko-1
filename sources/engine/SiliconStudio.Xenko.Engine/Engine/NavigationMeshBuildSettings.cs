﻿// Copyright (c) 2016 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using SiliconStudio.Core;
using SiliconStudio.Core.Annotations;

namespace SiliconStudio.Xenko.Engine
{
    /// <summary>
    /// Provides settings for the navigation mesh builder to control granularity and error margins 
    /// </summary>
    [DataContract]
    public struct NavigationMeshBuildSettings
    {
        /// <summary>
        /// The Height of a grid cell in the navigation mesh building steps using heightfields. 
        /// A lower number means higher precision on the vertical axis but longer build times
        /// </summary>
        [DataMemberRange(0.01, float.MaxValue)]
        public float CellHeight;

        /// <summary>
        /// The Width/Height of a grid cell in the navigation mesh building steps using heightfields. 
        /// A lower number means higher precision on the horizontal axes but longer build times
        /// </summary>
        [DataMemberRange(0.01, float.MaxValue)]
        public float CellSize;

        /// <summary>
        /// Tile size used for Navigation mesh tiles, the final size of a tile is CellSize*TileSize
        /// </summary>
        [DataMemberRange(8,4096,1,8)]
        public int TileSize;

        /// <summary>
        /// The minimum number of cells allowed to form isolated island areas
        /// </summary>
        [Display("Region Minimum Area")]
        [DataMemberRange(0, int.MaxValue)]
        public int RegionMinArea;

        /// <summary>
        /// Any regions with a span count smaller than this value will, if possible, 
        /// be merged with larger regions.
        /// </summary>
        [DataMemberRange(0, int.MaxValue)]
        public int RegionMergeArea;

        /// <summary>
        /// The maximum allowed length for contour edges along the border of the mesh.
        /// </summary>
        [Display("Edge Maximum Length")]
        [DataMemberRange(0, float.MaxValue)]
        public float EdgeMaxLen;

        /// <summary>
        /// The maximum distance a simplfied contour's border edges should deviate from the original raw contour.
        /// </summary>
        [Display("Edge Maximum Error")]
        [DataMemberRange(0.1, float.MaxValue)]
        public float EdgeMaxError;

        /// <summary>
        /// Sets the sampling distance to use when generating the detail mesh. (For height detail only.)
        /// </summary>
        [Display("Detail Sample Distance")]
        [DataMemberRange(1.0, float.MaxValue)]
        public float DetailSampleDist;

        /// <summary>
        /// The maximum distance the detail mesh surface should deviate from heightfield data. (For height detail only.)
        /// </summary>
        [Display("Detail Sample Maximum Error")]
        [DataMemberRange(0.0, float.MaxValue)]
        public float DetailSampleMaxError;
        
        public bool Equals(NavigationMeshBuildSettings other)
        {
            return CellHeight.Equals(other.CellHeight) && CellSize.Equals(other.CellSize) && TileSize == other.TileSize && RegionMinArea.Equals(other.RegionMinArea) && RegionMergeArea.Equals(other.RegionMergeArea) && EdgeMaxLen.Equals(other.EdgeMaxLen) && EdgeMaxError.Equals(other.EdgeMaxError) && DetailSampleDist.Equals(other.DetailSampleDist) && DetailSampleMaxError.Equals(other.DetailSampleMaxError);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is NavigationMeshBuildSettings && Equals((NavigationMeshBuildSettings)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CellHeight.GetHashCode();
                hashCode = (hashCode*397) ^ CellSize.GetHashCode();
                hashCode = (hashCode*397) ^ TileSize;
                hashCode = (hashCode*397) ^ RegionMinArea.GetHashCode();
                hashCode = (hashCode*397) ^ RegionMergeArea.GetHashCode();
                hashCode = (hashCode*397) ^ EdgeMaxLen.GetHashCode();
                hashCode = (hashCode*397) ^ EdgeMaxError.GetHashCode();
                hashCode = (hashCode*397) ^ DetailSampleDist.GetHashCode();
                hashCode = (hashCode*397) ^ DetailSampleMaxError.GetHashCode();
                return hashCode;
            }
        }
    }
}
