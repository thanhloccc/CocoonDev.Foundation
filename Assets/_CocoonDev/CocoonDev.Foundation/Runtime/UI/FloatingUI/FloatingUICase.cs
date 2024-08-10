using System;
using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;

namespace CocoonDev.Foundation
{
    partial struct FloatingUICase
    {
        public async UniTask FloatingCurveAsync(RectTransform rectTransform
            , CanvasGroup canvasGroup
            , Vector2 finalPosition
            , float duration
            , Action onComplete)
        {
            Vector2 originPosition = rectTransform.position;
            Vector2 positionCircle = originPosition.RandomPositionInCircle(250);

            Sequence sequence = Sequence.Create();
            await sequence.Chain(Tween.Position(rectTransform, positionCircle, duration * 30 / 100 , Ease.InQuad))
                .Group(Tween.Position(rectTransform, finalPosition, duration, Ease.OutQuad, startDelay: duration * 70 /100))
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
            await sequence.Group(Tween.Position(rectTransform, finalPotion, duration, Ease.InQuad))
                .Group(Tween.Alpha(canvasGroup, 0, 0.2F, Ease.OutQuad, startDelay: duration - 0.2F))
                .OnComplete(() => onComplete?.Invoke());
        }
    }
}
