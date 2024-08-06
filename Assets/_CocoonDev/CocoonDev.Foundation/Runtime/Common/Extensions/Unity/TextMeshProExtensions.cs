using Cysharp.Text;
using System.Text;
using System;
using TMPro;

namespace CocoonDev.Foundation
{ 
    public static class TextMeshProExtensions
    {
        //========
        // char[]
        //========

        public static void TrySetText(this TextMeshProUGUI textComponent, char[] text, int start, int length)
        {
            if (textComponent == null)
                throw new ArgumentNullException(nameof(textComponent));

            // Equality comparison of char[] and string thanks to ReadOnlySpan<char>.
            // The right hand side of the not equals operator is implicitly converted.
            if (new ReadOnlySpan<char>(text, start, length) != textComponent.text)
                textComponent.SetCharArray(text, start, start);
        }

        //====================
        // .NET StringBuilder
        //====================

        public static void TrySetText(this TextMeshProUGUI textComponent, StringBuilder stringBuilder)
        {
            if (textComponent == null)
                throw new ArgumentNullException(nameof(textComponent));

            if (stringBuilder == null)
                throw new ArgumentNullException(nameof(stringBuilder));

            // TextMeshPro has built in support for SetText(StringBuilder).
            if (!stringBuilder.Equals(textComponent.text))
                textComponent.SetText(stringBuilder);
        }

        //=================================
        // ZString Utf16ValueStringBuilder
        //=================================

        // ZString includes its own TextMeshPro extension methods for SetText, but it does not check whether the text has changed.
        public static void TrySetText(this TextMeshProUGUI textComponent, Utf16ValueStringBuilder stringBuilder)
        {
            if (textComponent == null)
                throw new ArgumentNullException(nameof(textComponent));

            // Aren't spans great? There may be another way to perform this comparison. I'm not a ZString expert.
            if (stringBuilder.AsSpan() != textComponent.text)
                textComponent.SetText(stringBuilder); // <- ZString extension method
        }
    }
}
