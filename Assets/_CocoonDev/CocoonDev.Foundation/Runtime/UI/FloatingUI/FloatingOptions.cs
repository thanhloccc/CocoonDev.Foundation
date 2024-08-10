using System;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public readonly struct FloatingOptions
    {
        public readonly FloatingMode FloatingMode;
        public readonly FloatingSettings Settings;

        public FloatingOptions(FloatingMode floatingMode
            , FloatingSettings settings)
        {
            FloatingMode = floatingMode;
            Settings = settings;
        }
    }

    public readonly struct FloatingSettings
    {
        public readonly FloatingMode FloatingMode;
        public readonly Vector2 FinalPosition;
        public readonly float Duration;
        public readonly Action OnComplete;

        public FloatingSettings(FloatingMode floatingMode
            , Vector2 finalPosition
            , float duration
            , Action onComplete)
        {
            FloatingMode = floatingMode;
            FinalPosition = finalPosition;
            Duration = duration;
            OnComplete = onComplete;
        }
    }

    public readonly struct FloatingUIData
    {
        public readonly int Value;
        public readonly Sprite Icon;

        public FloatingUIData(int value, Sprite icon)
        {
            Value = value;
            Icon = icon;
        }
    }

    public enum FloatingMode
    {
        None,
        Curve,
        Verticel,
    }
}
