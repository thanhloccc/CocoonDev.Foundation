using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public sealed class FloatingUIAnimateHelper
    {
        public static void Floating([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingOptions options)
        {
            switch (options.FloatingMode)
            {
                case FloatingMode.None:
                    Debug.LogWarning("[FloatingUI]");
                    break;

                case FloatingMode.Parabolic:
                    FloatingParabolic(rectTransform, canvasGroup, options.Settings);
                    break;

                case FloatingMode.Verticel:
                    FloatingVerticle(rectTransform, canvasGroup, options.Settings);
                    break;

                default:
                    break;
            }
        }

        public static async UniTask FloatingAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingOptions options
            , CancellationToken token)
        {
            switch (options.FloatingMode)
            {
                case FloatingMode.None:
                    Debug.LogWarning("[FloatingUI]");
                    break;

                case FloatingMode.Parabolic:
                    await FloatingParabolicAsync(rectTransform, canvasGroup, options.Settings, token);
                    break;

                case FloatingMode.Verticel:
                    await FloatingVerticleAsync(rectTransform, canvasGroup, options.Settings, token);
                    break;

                default:
                    break;
            }
        }

        public static void FloatingParabolic([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {
            FloatingUIAnimate.FloatingParabolic(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

        public static void FloatingVerticle([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {
            FloatingUIAnimate.FloatingVertical(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

        public static async UniTask FloatingParabolicAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {

            await FloatingUIAnimate.FloatingParabolicAsync(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

        public static async UniTask FloatingVerticleAsync([NotNull] RectTransform rectTransform
            , [NotNull] CanvasGroup canvasGroup
            , FloatingSettings settings)
        {

            await FloatingUIAnimate.FloatingVerticalAsync(rectTransform
                       , canvasGroup
                       , settings.FinalPosition
                       , settings.Duration
                       , settings.OnComplete);
        }

    }
}
