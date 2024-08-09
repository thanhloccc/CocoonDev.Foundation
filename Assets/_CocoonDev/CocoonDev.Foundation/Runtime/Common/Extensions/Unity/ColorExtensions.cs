using UnityEngine;

namespace CocoonDev.Foundation
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Sets the alpha value of the color using a byte.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="aValue">The alpha value as a byte.</param>
        /// <returns>The color with the new alpha value.</returns>
        public static Color WithAlpha(this Color color, byte aValue)
        {
            color.a = aValue / 255f;
            return color;
        }

        /// <summary>
        /// Sets the alpha value of the color using a float.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="aValue">The alpha value as a float.</param>
        /// <returns>The color with the new alpha value.</returns>
        public static Color WithAlpha(this Color color, float aValue)
        {
            color.a = aValue;
            return color;
        }

        /// <summary>
        /// Sets the alpha value of the color using an integer.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="aValue">The alpha value as an integer.</param>
        /// <returns>The color with the new alpha value.</returns>
        public static Color WithAlpha(this Color color, int aValue)
        {
            color.a = aValue / 255f;
            return color;
        }

    }
}
