// Copyright (c) 2014-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
#pragma warning disable 436 // The type 'type' in 'assembly' conflicts with the imported type 'type2' in 'assembly' (due to XenkoVersion being duplicated)

using System.Reflection;
using SiliconStudio;

[assembly: AssemblyCompany("Silicon Studio Corporation")]
[assembly: AssemblyCopyright("Copyright © 2011-2017 Silicon Studio Corp. All rights reserved.")]

[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]


[assembly: AssemblyVersion(XenkoVersion.PublicVersion)]
[assembly: AssemblyFileVersion(XenkoVersion.PublicVersion)]

[assembly: AssemblyInformationalVersion(XenkoVersion.AssemblyInformationalVersion)]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly:AssemblyConfiguration("Release")]
#endif

namespace SiliconStudio
{
    /// <summary>
    /// Internal version used to identify Xenko version.
    /// </summary>
    /// <remarks>
    /// Note: Xenko.xkpkg and PublicVersion versions should match.
    /// Also, during package build, PackageUpdateVersionTask is updating that file and expect some specific text regex so be careful if you change any of this.
    /// </remarks>
    internal class XenkoVersion
    {
        /// <summary>
        /// The copyright year or year range.
        /// </summary>
        public const string CopyrightYear = "2011-2017";

        /// <summary>
        /// The version used by editor for display purpose. 4th digit needs to be at least 1 if used (due to NuGet special cases).
        /// </summary>
        public const string PublicVersion = "2.1.2.1";

        /// <summary>
        /// The current assembly version as text, currently same as <see cref="PublicVersion"/>.
        /// </summary>
        public const string AssemblyVersion = PublicVersion;

        /// <summary>
        /// The NuGet package version without special tags.
        /// </summary>
        public const string NuGetVersionSimple = PublicVersion;

        /// <summary>
        /// The NuGet package version.
        /// </summary>
        public const string NuGetVersion = NuGetVersionSimple + NuGetVersionSuffix;

        /// <summary>
        /// The NuGet package suffix (i.e. -beta01). Note: might be replaced during package build.
        /// </summary>
        public const string NuGetVersionSuffix = "";

        /// <summary>
        /// The informational assembly version, containing -dev or -g[git_hash] during package.
        /// </summary>
        public const string AssemblyInformationalVersion = PublicVersion + AssemblyInformationalSuffix;

        /// <summary>
        /// The assembly suffix. Note: replaced by git commit during package build.
        /// </summary>
        private const string AssemblyInformationalSuffix = "-dev";
    }

    partial class PublicKeys
    {
#if SILICONSTUDIO_SIGNED
        public const string Default = ", PublicKey=0024000004800000940000000602000000240000525341310004000001000100f5ddb3ad5749f108242f29cfaa2205e4a6b87c7444314975dc0fbed53b7d638c17f9540763e7355be932481737cd97a4104aecda872c4805ed9473c70c239d8798b22aefc351bb2cc387eb4391f31c53aeb0452b89433562b06754af8e460384656cd388fb9bbfef348292f9fb4ee6d07b74a8490923079865a60238df259cd2";
#else
        public const string Default = "";
#endif
    }
}
