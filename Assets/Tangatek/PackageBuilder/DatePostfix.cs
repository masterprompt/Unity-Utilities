using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.PackageBuilder
{
    [CreateAssetMenu(menuName="Packages/Name Behaviors/Date Postfix")]
    public class DatePostfix : NameBehavior
    {
        public override string ProcessName(string name)
        {
            return name + DateTime.Now.ToString("_yyyyMMdd");
        }
    }
}