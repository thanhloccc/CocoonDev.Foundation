using Cysharp.Text;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public static class CurrenciesHelper
    {
        private static readonly string[] s_dIGITS = new string[] { "", "K", "M", "B", "T", "Qa" };

        public static Utf16ValueStringBuilder Format(int value)
        {
            float moneyRepresentation = value;
            int counter = 0;

            while (moneyRepresentation >= 1000)
            {
                moneyRepresentation /= 1000;
                counter++;
            }

            if (moneyRepresentation >= 100)
            {
                moneyRepresentation = Mathf.Floor(moneyRepresentation);
                if (counter != 0)
                {
                    // ZString
                    using (var stringBuilder = ZString.CreateStringBuilder())
                    {
                        stringBuilder.Append(moneyRepresentation, "F0");
                        stringBuilder.Append(GetDigits(counter));
                        return stringBuilder;
                    }

                    
                }

            }
            else if (moneyRepresentation >= 10)
            {
                // ZString
                using (var stringBuilder = ZString.CreateStringBuilder())
                {
                    stringBuilder.Append(moneyRepresentation, "F1");

                    if (stringBuilder.AsSpan()[stringBuilder.Length - 1] == '0')
                    {
                        stringBuilder.Remove(stringBuilder.Length - 2, 1);
                    }

                    if (counter != 0)
                    {
                        stringBuilder.Append(GetDigits(counter));
                        return stringBuilder;
                    }
                }
            }
            else
            {
                // ZString
                using (var stringBuilder = ZString.CreateStringBuilder())
                {
                    stringBuilder.Append(moneyRepresentation, "F2");

                    if (stringBuilder.AsSpan()[stringBuilder.Length - 1] == '0')
                    {
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                        if (stringBuilder.AsSpan()[stringBuilder.Length - 1] == '0')
                        {
                            stringBuilder.Remove(stringBuilder.Length - 2, 1);
                        }
                    }

                    if (counter != 0)
                    {
                        stringBuilder.Append(GetDigits(counter));
                        return stringBuilder;
                    }
                }
            }

            using (var stringBuilder = ZString.CreateStringBuilder())
            {
                stringBuilder.Append(moneyRepresentation);
                return stringBuilder;
            }
        }

        private static string GetDigits(int index)
        {
            if (index < 0 || index >= s_dIGITS.Length)
                return string.Empty;

            return s_dIGITS[index];
        }
    }

}
