using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CocoonDev.Foundation
{
    public class CurrenciesUIController : Singleton<CurrenciesUIController>
    {
        [SerializeField]
        private CurrencyUIPanel[] _currencyUIPanels;

        private Dictionary<CurrencyType, CurrencyUIPanel> _currencyUIPanelLinks;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _currencyUIPanelLinks = new Dictionary<CurrencyType, CurrencyUIPanel>();

            for (int i = 0; i < _currencyUIPanels.Length; i++)
            {
                var currencyUIPanel = _currencyUIPanels[i];
                if (!_currencyUIPanelLinks.ContainsKey(currencyUIPanel.CurrencyType))
                {
                    _currencyUIPanelLinks.Add(currencyUIPanel.CurrencyType, currencyUIPanel);
                    currencyUIPanel.Initialize(CurrenciesController.GetCurrencyByType(currencyUIPanel.CurrencyType));
                    currencyUIPanel.Show();
                }
            }
        }

        public CurrencyUIPanel GetCurrencyUIPanel(CurrencyType currencyType)
        {
            if(_currencyUIPanelLinks.TryGetValue(currencyType, out var currencyUIPanel)) 
                return currencyUIPanel;

            return null;
        }
    }
}
