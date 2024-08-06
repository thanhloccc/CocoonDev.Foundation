using System;
using UnityEngine;

namespace CocoonDev.Foundation
{
    [CreateAssetMenu(fileName = nameof(CurrenciesDatabase), menuName = "CocoonDev/Currencies")]
    public class CurrenciesDatabase : ScriptableObject
    {
        [SerializeField]
        private Currency[] _currencies;

        public ReadOnlyMemory<Currency> Currencies
        {
            get => _currencies;
        }
    }
}
