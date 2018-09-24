using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.PackageBuilder
{
    [CreateAssetMenu(menuName="Packages/Package Definition")]
    public class Package : ScriptableObject
    {
        public PackageConfiguration packageConfiguration;
    }
}