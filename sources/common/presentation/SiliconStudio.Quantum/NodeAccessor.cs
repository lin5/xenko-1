// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Diagnostics.Contracts;
using SiliconStudio.Core.Annotations;
using SiliconStudio.Core.Extensions;

namespace SiliconStudio.Quantum
{
    /// <summary>
    /// An object representing a single accessor of the value of a node, or one if its item.
    /// </summary>
    public struct NodeAccessor
    {
        /// <summary>
        /// The node of the accessor.
        /// </summary>
        public readonly IGraphNode Node;
        /// <summary>
        /// The index of the accessor.
        /// </summary>
        public readonly Index Index;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeAccessor"/> structure.
        /// </summary>
        /// <param name="node">The target node of this accessor.</param>
        /// <param name="index">The index of the target item if this accessor target an item. <see cref="Quantum.Index.Empty"/> otherwise.</param>
        public NodeAccessor([NotNull] IGraphNode node, Index index)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            Node = node;
            Index = index;
        }

        /// <summary>
        /// Retrieves the value backed by this accessor.
        /// </summary>
        /// <returns>The value backed by this accessor.</returns>
        [Pure]
        public object RetrieveValue() => Node.Retrieve(Index);

        /// <summary>
        /// Updates the value backed by this accessor.
        /// </summary>
        /// <param name="value">The new value to set.</param>
        public void UpdateValue(object value)
        {
            if (Index != Index.Empty)
            {
                ((IObjectNode)Node).Update(value, Index);
            }
            else
            {
                ((IMemberNode)Node).Update(value);
            }
        }

        /// <summary>
        /// Indicates whether this accessor can accept a value of the given type to update the targeted node.
        /// </summary>
        /// <param name="type">The type to evaluate.</param>
        /// <returns>True if this type is accepted, false otherwise.</returns>
        [Pure]
        public bool AcceptType(Type type)
        {
            return Node.Descriptor.GetInnerCollectionType().IsAssignableFrom(type);
        }

        /// <summary>
        /// Indicates whether this accessor can accept the given value to update the targeted node.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <returns>True if the value is accepted, false otherwise.</returns>
        [Pure]
        public bool AcceptValue(object value)
        {
            return value == null ? !Node.Descriptor.GetInnerCollectionType().IsValueType : Node.Descriptor.GetInnerCollectionType().IsInstanceOfType(value);
        }
    }
}
