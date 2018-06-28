using System;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Tangatek;
using UnityEngine;

namespace Source
{
    
    public static class PackageBuilder
    {
        private static string PackageName = "Assets/Packages/TangatekUtilities";
        
        private static ExportPackageOptions PackageOptions = ExportPackageOptions.IncludeDependencies;

        private static string PackageJsonPath = "../package.json";
        
        /// <summary>
        /// Valid file extensions to include in package
        /// </summary>
        private static string[] ValidFileExtensions = new string[]
        {
            ".cs"
        };
        
        /// <summary>
        /// Valid directories to include in package
        /// </summary>
        private static string[] ValidDirectories = new string[]
        {
            "Tangatek"
        };
        
        [MenuItem("Package Builder/Build Package")]
        public static void Build()
        {
            var filePathes = GetFilePathes().Select(MakeRelative);
            var version = LoadVersion(PackageJsonPath);
            AssetDatabase.ExportPackage(filePathes.ToArray(), PackageName + "." + version + ".unitypackage", PackageOptions);
        }

        private static IEnumerable<string> GetFilePathes()
        {
            var fileNames = new List<string>();
            ValidDirectories.ForEach(directoryName =>
            {
                fileNames.AddRange(GetFileNames(Application.dataPath + "/" + directoryName));
            });
            return fileNames;
        }

        private static IEnumerable<string> GetFileNames(string path)
        {
            var fileNames = new List<string>();
            fileNames.AddRange(GetValidFilesInPath(path));
            Directory.GetDirectories(path).ForEach(directoryPath =>
            {
                fileNames.AddRange(GetFileNames(directoryPath));
            });
            return fileNames;
        }

        private static IEnumerable<string> GetValidFilesInPath(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles().Select(fileInfo => fileInfo.ToString()).Where(IsValidPath);
        }
       
        private static bool IsValidPath(string path)
        {
            return IsValidExtension(Path.GetExtension(path));
        }
        
        private static bool IsValidExtension(string extension)
        {
            return ValidFileExtensions.Contains(extension);
        }

        private static string MakeRelative(string path)
        {
            var baseUri = new Uri(Application.dataPath);
            var pathUri = new Uri(path);
            return baseUri.MakeRelativeUri(pathUri).ToString();
        }

        private static string LoadVersion(string packageJsonPath)
        {
            string readText = File.ReadAllText(Application.dataPath + "/" + PackageJsonPath);
            
            return JsonUtility.FromJson<PackageJson>(readText).version;
        }

        private class PackageJson
        {
            public string version;
        }
    }
}