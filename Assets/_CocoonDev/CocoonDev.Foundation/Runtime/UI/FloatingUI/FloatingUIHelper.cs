using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public sealed class FloatingUIHelper
    {
        public static async UniTask FloatingAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {
            switch (settings.FloatingMode)
            {
                case FloatingMode.None:
                    Debug.LogWarning("[FloatingUI]");
                    break;

                case FloatingMode.Curve:
                    await FloatingParabolicAsync(rectTransform, canvasGroup, settings);
                    break;

                case FloatingMode.Verticel:
                    await FloatingVerticleAsync(rectTransform, canvasGroup, settings);
                    break;

                default:
                    break;
            }
        }
       

        public static async UniTask FloatingParabolicAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {

            await new FloatingUICase().FloatingCurveAsync(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

        public static async UniTask FloatingVerticleAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {

            await new FloatingUICase().FloatingVerticalAsync(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

    }
}
