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
        public readonly float Duration;
        public readonly Vector2 FinalPosition;
        public readonly Action OnComplete;

        public FloatingSettings(float duration
            , Vector2 finalPosition
            , Action onComplete)
        {
            Duration = duration;
            FinalPosition = finalPosition;
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
        Parabolic,
        Verticel,
    }
}
