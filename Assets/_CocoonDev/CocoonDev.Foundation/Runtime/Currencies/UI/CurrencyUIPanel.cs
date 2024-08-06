using System.Text;
using Cysharp.Text;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CocoonDev.Foundation
{
    public class CurrencyUIPanel : MonoBehaviour
    {
        [SerializeField]
        private CurrencyType _currencyType;

        [Space]
        [SerializeField]
        private bool _initialiseOnStart = true;
        [SerializeField]
        private bool _updateOnChange = true;
        [SerializeField]
        private bool _useFormattedAmount = true;

        [Space]
        [SerializeField, Required]
        private TextMeshProUGUI _textAmount;
        [SerializeField, Required]
        private Image _icon;

        private RectTransform _rectTransform;
        private Currency _currency;

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == false)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        public CurrencyType CurrencyType
        {
            get => _currencyType;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_initialiseOnStart)
            {
                Debug.Log("Check is here!");
            }
        }

        public void Initialize(Currency currency)
        {
            _currency = currency;

            Redraw();
            if (_updateOnChange)
            {
                _currency.OnCurrencyChange += OnCurrencyAmountChanged;
            }
        }

        public void Cleanup()
        {
            Hide();
            if (_updateOnChange)
            {
                _currency.OnCurrencyChange -= OnCurrencyAmountChanged;
            }
        }

        public void Redraw()
        {
            if (_useFormattedAmount)
            {
                _textAmount.TrySetText(_currency.AmountFormatted);
            }
            else
            {
                using (var stringBuiler = ZString.CreateStringBuilder())
                {
                    stringBuiler.Append(_currency.Amount);
                    _textAmount.TrySetText(stringBuiler);
                }
            }
          
        }

        public void SetAmount(int amount, bool format = true)
        {
        }

        public void Show()
        {
            
        }

        public void Hide()
        {
            
        }

        private void OnCurrencyAmountChanged(Currency currency, int amountDifference)
        {
            if (_useFormattedAmount)
            {
                _textAmount.TrySetText(_currency.AmountFormatted);
            }
            else
            {
                using (var stringBuiler = ZString.CreateStringBuilder())
                {
                    stringBuiler.Append(_currency.Amount);
                    _textAmount.TrySetText(stringBuiler);
                }
            }
        }
    }
}
