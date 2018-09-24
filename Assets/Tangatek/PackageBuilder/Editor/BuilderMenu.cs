using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tangatek.PackageBuilder
{
    public static class BuilderMenu
    {
        
        public static void BuildPackages()
        {
            //    Get a list of classes that extend Package
            //    Build each package
            var type = typeof(Package);
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && type != p)
                .ForEach(thisType =>
                {
                    Debug.Log(type.ToString());
                    
                });
        }

    }
}