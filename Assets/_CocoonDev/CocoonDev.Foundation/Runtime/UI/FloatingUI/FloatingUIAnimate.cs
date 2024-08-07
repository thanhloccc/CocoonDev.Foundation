using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;

namespace CocoonDev.Foundation
{
    internal struct FloatingUIAnimate
    {
        public static FloatingUIAnimate FloatingParabolic(RectTransform rectTransform
            , CanvasGroup canvasGroup
            , Vector2 finalPotion
            , float duration
            , Action onComplete)
        {
            Vector2 originPosition = rectTransform.position;
            Vector2 positionCircle = originPosition.RandomPositionInCircle(250);

            Sequence sequence = Sequence.Create();

            sequence.Chain(Tween.Position(rectTransform
                , positionCircle
                , duration - 1.0F
                , Ease.InQuad));

            sequence.Chain(Tween.Position(rectTransform
                , finalPotion
                , duration - 0.35F
                , Ease.OutQuad));

            sequence.Group(Tween.Alpha(canvasGroup
                , 0
                , 0.2F
                , Ease.OutQuad
                , startDelay: duration - 0.2F));

            sequence.OnComplete(onComplete);

            return new FloatingUIAnimate();
        }

        public static FloatingUIAnimate FloatingVertical(RectTransform rectTransform
            , CanvasGroup canvasGroup
            , Vector2 finalPotion
            , float duration
            , Action onComplete)
        {
            Sequence sequence = Sequence.Create();

            sequence.Chain(Tween.Position(rectTransform
                , finalPotion
                , duration, Ease.InQuad));

            sequence.Group(Tween.Alpha(canvasGroup
                , 0
                , 0.2F
                , Ease.OutQuad
                , startDelay: duration - 0.2F));

            sequence.OnComplete(onComplete);

            return new FloatingUIAnimate();
        }

        public static async UniTask FloatingParabolicAsync(RectTransform rectTransform
            , CanvasGroup canvasGroup
            , Vector2 finalPotion
            , float duration
            , Action onComplete
            , CancellationToken token)
        {
            Sequence sequence = Sequence.Create();

            await sequence.Chain(Tween.Position(rectTransform
                , finalPotion
                , duration, Ease.InQuad))
                .Group(Tween.Alpha(canvasGroup
                , 0
                , 0.2F
                , Ease.OutQuad
                , startDelay: duration - 0.2F))
                .OnComplete(() => onComplete?.Invoke())
                .WithCancellation(token);
           
        }

        public static async UniTask FloatingVerticalAsync(RectTransform rectTransform
            , CanvasGroup canvasGroup
            , Vector2 finalPotion
            , float duration
            , Action onComplete
            , CancellationToken token)
        {
            Sequence sequence = Sequence.Create();

            await sequence.Chain(Tween.Position(rectTransform
                , finalPotion
                , duration, Ease.InQuad))
                .Group(Tween.Alpha(canvasGroup
                , 0
                , 0.2F
                , Ease.OutQuad
                , startDelay: duration - 0.2F))
                .OnComplete(() => onComplete?.Invoke())
                .WithCancellation(token);
        }
    }
}
