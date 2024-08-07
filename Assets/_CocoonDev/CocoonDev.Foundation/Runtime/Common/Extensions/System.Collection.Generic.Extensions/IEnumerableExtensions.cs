using System.Collections.Generic;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public static class IEnumerableExtensions 
    {
        /// <summary>
        /// Check if array is null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return (array == null || array.Length == 0);
        }

        /// <summary>
        /// Check if list is null or empty
        /// </summary>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return (list == null || list.Count == 0);
        }

        /// <summary>
        /// Check if index is inside array range
        /// </summary>
        public static bool IsInRange<T>(this T[] array, int value)
        {
            return (value >= 0 && value < array.Length);
        }

        /// <summary>
        /// Check if index is inside list range
        /// </summary>
        public static bool IsInRange<T>(this List<T> list, int value)
        {
            return (value >= 0 && value < list.Count);
        }
    }
}
