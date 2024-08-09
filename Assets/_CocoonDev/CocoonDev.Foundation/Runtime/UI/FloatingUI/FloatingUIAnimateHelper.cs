using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public sealed class FloatingUIAnimateHelper
    {
        public static async UniTask FloatingAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingOptions options)
        {
            switch (options.FloatingMode)
            {
                case FloatingMode.None:
                    Debug.LogWarning("[FloatingUI]");
                    break;

                case FloatingMode.Parabolic:
                    await FloatingParabolicAsync(rectTransform, canvasGroup, options.Settings);
                    break;

                case FloatingMode.Verticel:
                    await FloatingVerticleAsync(rectTransform, canvasGroup, options.Settings);
                    break;

                default:
                    break;
            }
        }
       

        public static async UniTask FloatingParabolicAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {

            await new FloatingUIAnimate().FloatingParabolicAsync(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

        public static async UniTask FloatingVerticleAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {

            await new FloatingUIAnimate().FloatingVerticalAsync(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

    }
}
