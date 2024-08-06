using Cysharp.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CocoonDev.Foundation
{
    [System.Serializable]
    public class Currency
    {
        [SerializeField]
        private CurrencyType _currencyType;

        public CurrencyType CurrencyType
        {
            get => _currencyType;
        }

        public int Amount
        {
            get => _currentSave.Amount;
            set => _currentSave.Amount = value;
        }

        public Utf16ValueStringBuilder AmountFormatted
        {
            get => CurrenciesHelper.Format(_currentSave.Amount);
        }

        public event CurrencyChangeDelegate OnCurrencyChange;
        private CurrentSave _currentSave;

        public void Initialize()
        {

        }

        public void InvokeChangeEvent(int difference)
        {
            OnCurrencyChange?.Invoke(this, difference);
        }
    }

    [System.Serializable]
    public class CurrentSave
    {
        public int amount;

        public CurrentSave(int amount)
        {
            this.amount = amount;
        }

        public int Amount
        {
            get => amount;
            set => amount = value;
        }
    }

    public delegate void CurrencyChangeDelegate(Currency currency, int difference);
}
