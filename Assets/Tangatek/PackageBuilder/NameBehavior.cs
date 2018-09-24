using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.PackageBuilder
{
    public abstract class NameBehavior : ScriptableObject
    {
        public abstract string ProcessName(string name);
    }
}