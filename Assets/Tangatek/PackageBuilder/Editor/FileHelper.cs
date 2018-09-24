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
    public static class FileHelper
    {
        /// <summary>
        /// Returns a list of file names, recursively, for a path
        /// </summary>
        /// <param name="path">valid directory path
        /// </param>
        /// <returns></returns>
        public static IEnumerable<string> GetRecursiveFileNames(string path)
        {
            var fileNames = new List<string>();
            //    Get list of files
            var directoryInfo = new DirectoryInfo(path);
            var directoryFileNames = directoryInfo.GetFiles().Select(fileInfo => fileInfo.ToString());
            fileNames.AddRange(directoryFileNames);
            //    Get each sub directory's file names
            Directory.GetDirectories(path).ForEach(directoryPath =>
            {
                fileNames.AddRange(GetRecursiveFileNames(directoryPath));
            });
            return fileNames;
        }
        
        /// <summary>
        /// Makes file path relative to application data path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MakePathRelative(string path)
        {
            var baseUri = new Uri(Application.dataPath);
            var pathUri = new Uri(path);
            return baseUri.MakeRelativeUri(pathUri).ToString();
        }
        
        /// <summary>
        /// Returns lambda that will return true if filePath contains extension from provided extensions,  This is used in linq chains
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public static Func<string, bool> IncludesExtensions(IEnumerable<string> extensions)
        {
            return filePath =>
            {
                var extension = Path.GetExtension(filePath);
                return extensions.Contains(extension);
            };
        }
        
        /// <summary>
        /// Returns lambda that will return true if filePath does not contain extension from provided extensions,  This is used in linq chains
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public static Func<string, bool> ExcludesExtensions(IEnumerable<string> extensions)
        {
            return filePath =>
            {
                var extension = Path.GetExtension(filePath);
                return !extensions.Contains(extension);
            };
        }
    }
}