using Cysharp.Text;
using UnityEngine;

namespace CocoonDev.Foundation
{
    [System.Serializable]
    public class Currency
    {
        [SerializeField]
        private CurrencyType _currencyType;
        [SerializeField]
        private int _amount;

        public CurrencyType CurrencyType
        {
            get => _currencyType;
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public Utf16ValueStringBuilder AmountFormatted
        {
            get => CurrenciesHelper.Format(_amount);
        }

        public event CurrencyChangeDelegate OnCurrencyChange;
       

        public void InvokeChangeEvent(int difference)
        {
            OnCurrencyChange?.Invoke(this, difference);
        }
    }

    public delegate void CurrencyChangeDelegate(Currency currency, int difference);
}
