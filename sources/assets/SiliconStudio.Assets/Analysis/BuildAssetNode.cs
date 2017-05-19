﻿// Copyright (c) 2011-2017 Silicon Studio Corp. All rights reserved. (https://www.siliconstudio.co.jp)
// See LICENSE.md for full license information.
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SiliconStudio.Assets.Compiler;
using SiliconStudio.Core.Serialization.Contents;

namespace SiliconStudio.Assets.Analysis
{
    public class BuildAssetNode
    {
        private readonly BuildDependencyManager buildDependencyManager;
        private readonly ConcurrentDictionary<AssetId, BuildAssetNode> references = new ConcurrentDictionary<AssetId, BuildAssetNode>();
        private readonly ConcurrentDictionary<BuildAssetNode, AssetId> referencedBy = new ConcurrentDictionary<BuildAssetNode, AssetId>();
        private long version = -1;

        public AssetItem AssetItem { get; }

        public BuildDependencyType DependencyType { get; }

        public ICollection<BuildAssetNode> References => references.Values;

        public ICollection<BuildAssetNode> ReferencedBy => referencedBy.Keys;

        public BuildAssetNode(AssetItem assetItem, BuildDependencyType type, BuildDependencyManager dependencyManager)
        {
            AssetItem = assetItem;
            DependencyType = type;
            buildDependencyManager = dependencyManager;
        }

        /// <summary>
        /// Performs analysis on the asset to figure out all the needed dependencies
        /// </summary>
        /// <param name="context">The compiler context</param>
        /// <param name="typesToInclude">The types to add as dependency from parents</param>
        /// <param name="typesToExclude">The types to not mark as dependency</param>
        public void Analyze(AssetCompilerContext context, HashSet<KeyValuePair<Type, BuildDependencyType>> typesToInclude, HashSet<Type> typesToExclude = null)
        {
            var mainCompiler = BuildDependencyManager.AssetCompilerRegistry.GetCompiler(AssetItem.Asset.GetType(), buildDependencyManager.CompilationContext);
            if (mainCompiler == null) return; //scripts and such don't have compiler

            if (typesToExclude != null)
            {
                foreach (var type in mainCompiler.GetInputTypesToExclude(context, AssetItem))
                {
                    typesToExclude.Add(type);
                }
            }
            if (typesToInclude != null)
            {
                foreach (var inputType in mainCompiler.GetInputTypes(context, AssetItem))
                {
                    typesToInclude.Add(inputType);
                }
            }

            var assetVersion = AssetItem.Version;
            if (Interlocked.Exchange(ref version, assetVersion) == assetVersion) return; //same version, skip analysis, do not clear links

            //rebuild the dependency links, we clean first
            //remove self from current childs
            foreach (var buildAssetNode in references)
            {
                referencedBy.TryRemove(buildAssetNode.Value, out AssetId _);
            }
            //clean up our references
            references.Clear();

            //DependencyManager check
            //for now we use the dependency manager itself to resolve runtime dependencies, in the future we might want to unify the builddependency manager with the dependency manager
            var dependencies = AssetItem.Package.Session.DependencyManager.ComputeDependencies(AssetItem.Id, AssetDependencySearchOptions.Out);
            foreach (var assetDependency in dependencies.LinksOut)
            {
                var assetType = assetDependency.Item.Asset.GetType();
                if (typesToExclude == null || !typesToExclude.Contains(assetType)) //filter out what we do not need
                {
                    if (typesToInclude != null)
                    {
                        foreach (var input in typesToInclude.Where(x => x.Key == assetType))
                        {
                            var dependencyType = input.Value;
                            var node = buildDependencyManager.FindOrCreateNode(assetDependency.Item, dependencyType);
                            references.TryAdd(assetDependency.Item.Id, node);
                            node.referencedBy.TryAdd(this, AssetItem.Id); //add this as referenced by child
                        }
                    }
                    else
                    {
                        foreach (var inputType in mainCompiler.GetInputTypes(context, AssetItem)) //resolve by type since dependency manager will provide us the assets needed
                        {
                            if (inputType.Key == assetType)
                            {
                                var dependencyType = inputType.Value;
                                var node = buildDependencyManager.FindOrCreateNode(assetDependency.Item, dependencyType);
                                references.TryAdd(assetDependency.Item.Id, node);
                                node.referencedBy.TryAdd(this, AssetItem.Id); //add this as referenced by child
                            }
                        }
                    }
                }
            }

            //Input files required
            foreach (var inputFile in mainCompiler.GetInputFiles(context, AssetItem)) //directly resolve by input files, in the future we might just want this pass
            {
                if (inputFile.Type == UrlType.Content || inputFile.Type == UrlType.ContentLink)
                {
                    var asset = AssetItem.Package.Session.FindAsset(inputFile.Path); //this will search all packages
                    if (asset == null) continue; //this might be an error tho... but in the end compilation might fail so we let the build engine do the error reporting if it really was a issue
                    if (typesToExclude == null || !typesToExclude.Contains(asset.GetType()))
                    {
                        var dependencyType = inputFile.Type == UrlType.Content ? BuildDependencyType.CompileContent : BuildDependencyType.CompileAsset; //Content means we need to load the content, the rest is just asset dependency
                        var node = buildDependencyManager.FindOrCreateNode(asset, dependencyType);
                        references.TryAdd(asset.Id, node);
                        node.referencedBy.TryAdd(this, AssetItem.Id); //add this as referenced by child
                    }
                }
            }
        }
    }
}
