using System;
using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;

namespace CocoonDev.Foundation
{
    internal struct FloatingUIAnimate
    {
        public async UniTask FloatingParabolicAsync(RectTransform rectTransform
            , CanvasGroup canvasGroup
            , Vector2 finalPotion
            , float duration
            , Action onComplete)
        {
            Sequence sequence = Sequence.Create();

            Vector2 positionCircle = (Vector2)rectTransform.position;
            positionCircle = positionCircle.RandomPositionInCircle(250);

            float totalDuration = duration;
            float durationPath1 = totalDuration * 30 / 100;
            float durationPath2 = totalDuration * 70 / 100;

            await sequence.Chain(Tween.Position(rectTransform, positionCircle, durationPath1, Ease.InQuad))
                 .Chain(Tween.Position(rectTransform, finalPotion, durationPath2, Ease.InQuad))
                 .Group(Tween.Alpha(canvasGroup, 0, 0.2F, Ease.OutQuad, startDelay: duration - 0.2F))
                 .OnComplete(() => onComplete?.Invoke());

        }

        public async UniTask FloatingVerticalAsync(RectTransform rectTransform
            , CanvasGroup canvasGroup
            , Vector2 finalPotion
            , float duration
            , Action onComplete)
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
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}
