using System.Collections.Generic;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public class CurrenciesUIController : MonoBehaviour
    {
        [SerializeField]
        private CurrencyUIPanel[] _currencyUIPanels;

        private static Dictionary<CurrencyType, CurrencyUIPanel> s_currencyUIPanelCacheById  = new();


#if UNITY_EDITOR
        /// <seealso href="https://docs.unity3d.com/Manual/DomainReloading.html"/>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            s_currencyUIPanelCacheById = new();
        }
#endif

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < _currencyUIPanels.Length; i++)
            {
                var currencyUIPanel = _currencyUIPanels[i];
                if (!s_currencyUIPanelCacheById.ContainsKey(currencyUIPanel.CurrencyType))
                {
                    s_currencyUIPanelCacheById.Add(currencyUIPanel.CurrencyType, currencyUIPanel);
                    currencyUIPanel.Initialize();
                }
            }
        }

        #region Static Methods
        public CurrencyUIPanel Of(CurrencyType currencyType)
        {
            if (s_currencyUIPanelCacheById.TryGetValue(currencyType, out var currencyUIPanel))
                return currencyUIPanel;

            return null;
        }
        #endregion

    }
}
