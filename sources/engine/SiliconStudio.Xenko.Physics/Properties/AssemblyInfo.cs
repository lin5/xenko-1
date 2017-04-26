// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyProduct("SiliconStudio.Xenko.Physics")]
[assembly: AssemblyTitle("SiliconStudio.Xenko.Physics")]
[assembly: AssemblyDescription("Xenko physics core assembly")]

#pragma warning disable 436 // SiliconStudio.PublicKeys is defined in multiple assemblies

[assembly: InternalsVisibleTo("SiliconStudio.Xenko.Engine" + SiliconStudio.PublicKeys.Default)]
