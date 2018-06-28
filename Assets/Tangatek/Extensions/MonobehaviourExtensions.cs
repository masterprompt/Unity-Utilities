using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tangatek
{
    /// <summary>
    /// Extensions for Monobehaviours
    /// </summary>
    public static class MonobehaviourExtensions
    {
        /// <summary>
        /// Retrieves components which have an interface on them (hack for retrieving interfaces)
        /// </summary>
        /// <param name="behaviour"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetInterfacesInChildren<T>(this MonoBehaviour behaviour)
        {
            return behaviour.GetComponentsInChildren<MonoBehaviour>().OfType<T>();
        }
        /// <summary>
        /// Retrieves component that has an interface on them (hack for retrieving interfaces)
        /// </summary>
        /// <param name="behaviour"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetInterfaces<T>(this MonoBehaviour behaviour)
        {
            return behaviour.GetComponents<MonoBehaviour>().OfType<T>();
        }

        /// <summary>
        /// Convienance method for Enable
        /// </summary>
        /// <param name="monoBehaviour"></param>
        public static void Enable(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.enabled = true;
        }

        /// <summary>
        /// Convienance method for disable
        /// </summary>
        /// <param name="monoBehaviour"></param>
        public static void Disable(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.enabled = false;
        }
    }
}