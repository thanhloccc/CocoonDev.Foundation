using UnityEngine;

namespace CocoonDev.Foundation
{
        public static class CurrencyUIHelper
        {
                public const float PANEL_HEIGHT = 50f;

                private static readonly Vector2[] PANEL_SIZES = new Vector2[]
                {
                        new Vector2(-390F, 50),
                        new Vector2(390, 50),
                        new Vector2(50, 30),
                        new Vector2(390, 30),
                 };

                public static Vector2 GetPanelSize(int charactersCount)
                {
                        if (PANEL_SIZES.IsInRange(charactersCount))
                                return PANEL_SIZES[charactersCount];

                        return PANEL_SIZES[PANEL_SIZES.Length - 1];
                }
        }
}
