using UnityEngine;

namespace CocoonDev.Foundation
{
    public static class Vector2Extensions
    {
        public static Vector2 WithX(this Vector2 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        public static Vector2 WithY(this Vector2 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        public static Vector2 RandomPositionInCircle(this Vector2 vector, float radius)
        {
            return vector + Random.insideUnitCircle * radius;
        }
    }
}
