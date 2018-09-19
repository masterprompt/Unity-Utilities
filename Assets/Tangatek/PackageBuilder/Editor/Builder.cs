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
        private Package package;
        
        [MenuItem("Package Builder/Build Packages")]
        public static void BuildPackages()
        {
            //    Get a list of classes that extend Package
            //    Build each package
        }

        public void Build()
        {
            var filePathes = GetFiles().Select(MakeRelative);
            var packagePath = package.ExportPath + package.Name + "." + package.Version + "." + package.Extension;
            AssetDatabase.ExportPackage(filePathes.ToArray(), packagePath, package.ExportOptions);
        }

        private IEnumerable<string> GetFiles()
        {
            var fileNames = new List<string>();
            package.ValidDirectories.ForEach(directoryName =>
            {
                fileNames.AddRange(GetFileNames(Application.dataPath + "/" + directoryName));
            });
            return fileNames;
        }

        private IEnumerable<string> GetFileNames(string path)
        {
            var fileNames = new List<string>();
            fileNames.AddRange(GetValidFilesInPath(path));
            Directory.GetDirectories(path).ForEach(directoryPath =>
            {
                fileNames.AddRange(GetFileNames(directoryPath));
            });
            return fileNames;
        }

        private IEnumerable<string> GetValidFilesInPath(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles().Select(fileInfo => fileInfo.ToString()).Where(IsValidPath);
        }
       
        private bool IsValidPath(string path)
        {
            return IsValidExtension(Path.GetExtension(path));
        }
        
        private bool IsValidExtension(string extension)
        {
            return package.ValidFileExtensions.Contains(extension);
        }

        private static string MakeRelative(string path)
        {
            var baseUri = new Uri(Application.dataPath);
            var pathUri = new Uri(path);
            return baseUri.MakeRelativeUri(pathUri).ToString();
        }
    }
}