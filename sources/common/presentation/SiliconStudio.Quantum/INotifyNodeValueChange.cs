// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;

namespace SiliconStudio.Quantum
{
    /// <summary>
    /// An interface representing an object notifying changes when the value of a related node changes.
    /// </summary>
    public interface INotifyNodeValueChange
    {
        /// <summary>
        /// Raised just before a change to the value of a related node occurs.
        /// </summary>
        event EventHandler<MemberNodeChangeEventArgs> ValueChanging;

        /// <summary>
        /// Raised when a change to the value of a related node has occurred.
        /// </summary>
        event EventHandler<MemberNodeChangeEventArgs> ValueChanged;
    }
}
