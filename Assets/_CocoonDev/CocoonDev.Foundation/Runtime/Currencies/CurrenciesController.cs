using System.Collections.Generic;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public class CurrenciesController : MonoBehaviour
    {
        private static CurrenciesController s_instance;

        [SerializeField]
        private Currency[] _currencies;

        private static Dictionary<CurrencyType, Currency> s_currenyCacheBuyId = new Dictionary<CurrencyType, Currency>();

        private static bool s_isInitialized;

#if UNITY_EDITOR
        /// <seealso href="https://docs.unity3d.com/Manual/DomainReloading.html"/>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            s_isInitialized = false;
            s_currenyCacheBuyId = new();
        }
   #endif

        private void Awake()
        {
            Initialize();
            LoadCurrency();
        }

        private void OnApplicationFocus(bool focus)
        {
#if !UNITY_EDITOR
            if (!focus)
            {
                SaveCurrency();
            }
#endif
        }


        private void OnDestroy()
        {
#if UNITY_EDITOR
            SaveCurrency();
#endif
        }

        public void Initialize()
        {
            if (s_isInitialized)
                return;

            s_instance = this;
           
            for (int i = 0; i < _currencies.Length; i++)
            {
                if (!s_currenyCacheBuyId.ContainsKey(_currencies[i].CurrencyType))
                {
                    s_currenyCacheBuyId.Add(_currencies[i].CurrencyType, _currencies[i]);
                }
                else
                {
                    Debug.LogError(string.Format("[Currency Syste]: Currency with type {0} added to database twice!", _currencies[i].CurrencyType));
                }
            }

            s_isInitialized = true;

        }

        #region Static Methods
        public static bool HasAmount(CurrencyType currencyType, int amount)
        {
            if (s_currenyCacheBuyId.TryGetValue(currencyType, out var currency))
            {
                return currency.Amount >= amount;
            }


            return false;
        }

        public static int GetAmountById(CurrencyType currencyType)
        {
            if (s_currenyCacheBuyId.TryGetValue(currencyType, out var currency))
            {
                return currency.Amount;
            }

            return 0;
        }

        public static Currency Of(CurrencyType currencyType)
        {
            if (s_currenyCacheBuyId.TryGetValue(currencyType, out var currency))
            {
                return currency;
            }

            return null;
        }

        public static void Set(CurrencyType currencyType, int amount)
        {
            if (s_currenyCacheBuyId.TryGetValue(currencyType, out var currency))
            {
                currency.Amount = amount;

                // Invoke currency change event
                currency.InvokeChangeEvent(0);

                return;
            }

            Debug.LogError("Not found!");

        }

        public static void Add(CurrencyType currencyType, int amount)
        {
            if (s_currenyCacheBuyId.TryGetValue(currencyType, out var currency))
            {
                currency.Amount += amount;

                // Invoke currency change event
                currency.InvokeChangeEvent(amount);

                return;
            }

            Debug.LogError("Not found!");
        }

        public static void Substract(CurrencyType currencyType, int amount)
        {
            if (s_currenyCacheBuyId.TryGetValue(currencyType, out var currency))
            {
                currency.Amount -= amount;

                // Invoke currency change event
                currency.InvokeChangeEvent(-amount);

                return;
            }

            Debug.LogError("Not found!");
        }

        #endregion


        // Save currency amounts to PlayerPrefs
        private void SaveCurrency()
        {
            foreach (var currency in _currencies)
            {
                PlayerPrefs.SetInt(currency.CurrencyType.ToString(), currency.Amount);
            }
            PlayerPrefs.Save();
        }

        // Load currency amounts from PlayerPrefs
        private void LoadCurrency()
        {
            foreach(var currency in _currencies)
            {
                if (PlayerPrefs.HasKey(currency.CurrencyType.ToString()))
                {
                    s_currenyCacheBuyId[currency.CurrencyType].Amount = PlayerPrefs.GetInt(currency.CurrencyType.ToString());
                }
            }
            
        }
    }
}
