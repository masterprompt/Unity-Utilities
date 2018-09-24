using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.PackageBuilder
{
    [CreateAssetMenu(menuName="Packages/Package Configuration")]
    public class PackageConfiguration : ScriptableObject
    {
        private static string ExportPath = "Assets/Packages/";
        private static string ExportExtension = ".unitypackage";
        
        public List<string> importDirectories = new List<string>();
        public List<string> includedExtensions = new List<string>();
        public List<string> excludeExtensions = new List<string>();
        public List<NameBehavior> nameBehaviors = new List<NameBehavior>();
        
        public string exportPath = ExportPath;
        public string exportExtension = ExportExtension;
    }
}