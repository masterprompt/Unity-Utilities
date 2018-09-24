using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Tangatek
{
    /// <summary>
    /// Extensions for IEnumerable<T>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Iterates over a collection applying an action to each item
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach(var item in enumerable)
                action(item);
        }
        
                
        /// <summary>
        /// Returns a random item from a collection
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("Can't get random of null");
            }

            // note: creating a Random instance each call may not be correct for you,
            // consider a thread-safe static instance
            //var r = new Random();  
            var list = enumerable as IList<T> ?? enumerable.ToList(); 
            return list.Count == 0 ? default(T) : list[Mathf.FloorToInt(UnityEngine.Random.Range(0, list.Count))];
        }

        /// <summary>
        /// Flattens a 2D array
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        {
            var list = new List<T>();
            source.ForEach(item => list.AddRange(item));
            return list;
        }
    }
}