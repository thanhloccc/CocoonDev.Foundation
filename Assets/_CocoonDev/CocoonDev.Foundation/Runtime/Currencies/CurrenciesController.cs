using System;
using System.Collections.Generic;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public class CurrenciesController : MonoBehaviour
    {
        private static CurrenciesController s_instance;

        [SerializeField]
        private CurrenciesDatabase _currenciesDatabase;

        private static ReadOnlyMemory<Currency> s_currencies;
        public static ReadOnlyMemory<Currency> Currencies
        {
            get => s_currencies;
        }

        private static readonly Dictionary<CurrencyType, int> s_currenciesLinks = new Dictionary<CurrencyType, int>();

        private static bool s_isInitialized;
        private static event Action onInital;

        private void Awake()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            if (s_isInitialized)
                return;

            s_instance = this;

            // Inialize database
            //_currenciesDatabase.Initialize();

            // Store active currencies
            s_currencies = _currenciesDatabase.Currencies;

            // Link currecies by the type
            for (int i = 0; i < s_currencies.Length; i++)
            {
                if (!s_currenciesLinks.ContainsKey(s_currencies.Span[i].CurrencyType))
                {
                    s_currenciesLinks.Add(s_currencies.Span[i].CurrencyType, i);
                }
                else
                {
                    Debug.LogError(string.Format("[Currency Syste]: Currency with type {0} added to database twice!", s_currencies.Span[i].CurrencyType));
                }
            }

            s_isInitialized = true;

            onInital?.Invoke();
            onInital = null;

        }

        public static bool HasAmount(CurrencyType currencyType, int amount)
        {
            int link = FindLinkByType(currencyType);
            if (link == -1)
            {
                Debug.LogError("Not found");
                return false;
            }

            return s_currencies.Span[link].Amount >= amount;
        }

        public static int GetAmountByType(CurrencyType currencyType)
        {
            int link = FindLinkByType(currencyType);
            if (link == -1)
            {
                Debug.LogError("Not found");
                return -1;
            }
            return s_currencies.Span[link].Amount;
        }

        public static Currency GetCurrencyByType(CurrencyType currencyType)
        {
            int link = FindLinkByType(currencyType);
            if (link == -1)
            {
                Debug.LogError("Not found");
                return null;
            }

            return s_currencies.Span[link];
        }

        public static void Set(CurrencyType currencyType, int amount)
        {
            int link = FindLinkByType(currencyType);
            if (link == -1)
            {
                Debug.LogError("Not found");
                return;
            }

            var currency = s_currencies.Span[link];
            currency.Amount = amount;

            // Change save state to required

            // Invoke currency change event
            currency.InvokeChangeEvent(0);
        }

        public static void Add(CurrencyType currencyType, int amount)
        {
            int link = FindLinkByType(currencyType);

            var currency = s_currencies.Span[link];
            currency.Amount += amount;

            // Change save state to required

            // Invoke currency change event
            currency.InvokeChangeEvent(amount);
        }

        public static void Substract(CurrencyType currencyType, int amount)
        {
            int link = FindLinkByType(currencyType);

            var currency = s_currencies.Span[link];

            currency.Amount -= amount;

            // Change save to required

            // Invoke currency change event
            currency.InvokeChangeEvent(-amount);
        }

        public static void SubscribeGlobalCallback(CurrencyChangeDelegate currencyChange)
        {
            for (int i = 0; i < s_currencies.Length; i++)
            {
                s_currencies.Span[i].OnCurrencyChange += currencyChange;
            }
        }

        public static void UnsubscribeGlobalCallback(CurrencyChangeDelegate currencyChange)
        {
            for (int i = 0; i < s_currencies.Length; i++)
            {
                s_currencies.Span[i].OnCurrencyChange -= currencyChange;
            }
        }

        public static void InvokeOrSubcrtibe(Action callback)
        {
            if (s_isInitialized)
            {
                callback?.Invoke();
            }
            else
            {
                onInital += callback;
            }
        }

        private static int FindLinkByType(CurrencyType currencyType)
        {
            if (s_currenciesLinks.TryGetValue(currencyType, out var link))
                return link;

            return -1;
        }
    }
}
