using System;
using System.Collections.Generic;
using UnityEditor;

namespace Tangatek.PackageBuilder
{
    public abstract class Package
    {
        /// <summary>
        /// Path to export to
        /// </summary>
        public virtual string ExportPath
        {
            get { return "Assets/Packages/"; }
        }

        /// <summary>
        /// Exported name of package (format: Name.Version.Extension)
        /// </summary>
        public virtual string Name
        {
            get { return "PackageName"; }
        }
        
        /// <summary>
        /// Exported extension of package (format: Name.Version.Extension)
        /// </summary>
        public virtual string Extension
        {
            get { return "unity"; }
        }

        /// <summary>
        /// Export options
        /// </summary>
        public virtual ExportPackageOptions ExportOptions
        {
            get { return ExportPackageOptions.IncludeDependencies; }
        }

        /// <summary>
        /// List of valid file extensions in valid directories
        /// </summary>
        public virtual IEnumerable<string> ValidFileExtensions
        {
            get
            {
                return new string[]
                {
                    ".cs"
                };
            }
        }

        /// <summary>
        /// List of directories to include (under Assets)
        /// </summary>
        public virtual IEnumerable<string> ValidDirectories
        {
            get
            {
                return new string[]
                {
                    "Tangatek"
                };
            }
        }

        /// <summary>
        /// Version information added to package name (format: Name.Version.Extension)
        /// </summary>
        public virtual string Version
        {
            get { return DateTime.Now.ToString("yyyy/MM/dd"); }
        }
    }
}