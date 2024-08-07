using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public static class Vector3Extensions
    {
        public static Vector3 WithX(this Vector3 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        public static Vector3 WithY(this Vector3 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        public static Vector3 AddX(this Vector3 vector, float x)
        {
            vector.x += x;
            return vector;
        }

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }

        public static Vector3 AddZ(this Vector3 vector, float z)
        {
            vector.z += z;
            return vector;
        }

        public static Vector3 SubtractX(this Vector3 vector, float x)
        {
            vector.x -= x;
            return vector;
        }

        public static Vector3 SubtractY(this Vector3 vector, float y)
        {
            vector.y -= y;
            return vector;
        }

        public static Vector3 SubtractZ(this Vector3 vector, float z)
        {
            vector.z -= z;
            return vector;
        }



        /// <summary>
        /// Convert int value to Vector3
        /// </summary>
        /// <param name="value">value to convert</param>
        /// <returns></returns>
        public static Vector3 ToVector3(this int value)
        {
            return new Vector3(value, value, value);
        }
    }
}
