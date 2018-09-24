using System;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Tangatek;
using UnityEngine;

namespace Tangatek.PackageBuilder
{
    public class Builder
    {
        private static ExportPackageOptions exportPackageOptions = ExportPackageOptions.IncludeDependencies;

        public static void Build(Package package)
        {
            if(package.packageConfiguration==null) throw new Exception("No package configuration found.");
            //    If we don't have a list of directories in the package, then use the package location
            var validDirectories = package.packageConfiguration.importDirectories.Count > 0
                ? package.packageConfiguration.importDirectories
                : new List<string>()
                {
                    Path.GetDirectoryName(AssetDatabase.GetAssetPath(package))
                };
            //    Get list of files from directories
            var  fileNames = validDirectories.Select(FileHelper.GetRecursiveFileNames)
                .Flatten()
                .Select(FileHelper.MakePathRelative);
            
            //    Filter out excluded extensions
            if (package.packageConfiguration.excludeExtensions.Count > 0) fileNames = fileNames.Where(FileHelper.ExcludesExtensions(package.packageConfiguration.excludeExtensions));
            //    Filter out any extension that isn't in the included extensions list
            if (package.packageConfiguration.includedExtensions.Count > 0) fileNames = fileNames.Where(FileHelper.IncludesExtensions(package.packageConfiguration.includedExtensions));
            var packageName = package.name;
            foreach (var nameBehavior in package.packageConfiguration.nameBehaviors)
                packageName = nameBehavior.ProcessName(packageName);
            var packagePath = package.packageConfiguration.exportPath + packageName + package.packageConfiguration.exportExtension;
            Directory.CreateDirectory(package.packageConfiguration.exportPath);
            AssetDatabase.ExportPackage(fileNames.ToArray(), packagePath, exportPackageOptions);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Build all packages in project
        /// </summary>
        [MenuItem("Package Builder/Build All Packages")]
        public static void BuildAll()
        {
            AssetDatabase
                .FindAssets("t:Package", null)
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(LoadAsset<Package>)
                .ForEach(Build);
        }

        /// <summary>
        /// Convienence method for loading an asset
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T LoadAsset<T>(string path) where T: UnityEngine.Object
        {
            return (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
    }
}