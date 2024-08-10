using Cysharp.Text;
using PrimeTween;
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

        [SerializeField]
        private bool _updateOnChange = true;
        [SerializeField]
        private bool _useFormattedAmount = true;
        [SerializeField]
        private bool _applyTween;

        [Space]
        [SerializeField, Required]
        private RectTransform _rectTransform;
        [SerializeField, Required]
        private TextMeshProUGUI _textAmount;

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

       
        public void Initialize()
        {

            Redraw();
            if (_updateOnChange)
            {
                CurrenciesController.Of(_currencyType).OnCurrencyChange += OnCurrencyAmountChanged;
            }
        }

        public void Cleanup()
        {
       
            if (_updateOnChange)
            {
                CurrenciesController.Of(_currencyType).OnCurrencyChange -= OnCurrencyAmountChanged;
            }
        }

        public void Redraw()
        {
            if (_useFormattedAmount)
            {
                _textAmount.TrySetText(CurrenciesController.Of(_currencyType).AmountFormatted);
            }
            else
            {
                using (var stringBuiler = ZString.CreateStringBuilder())
                {
                    stringBuiler.Append(CurrenciesController.GetAmountById(_currencyType));
                    _textAmount.TrySetText(stringBuiler);
                }
            }
          
        }


        private void OnCurrencyAmountChanged(Currency currency, int amountDifference)
        {
            if (_useFormattedAmount)
            {
                _textAmount.TrySetText(currency.AmountFormatted);
            }
            else
            {
                using (var stringBuiler = ZString.CreateStringBuilder())
                {
                    stringBuiler.Append(currency.Amount);
                    _textAmount.TrySetText(stringBuiler);
                }
            }

            if (_applyTween)
            {
                var sequence = Sequence.Create()
                .Chain(Tween.Scale(_rectTransform, 1.1F, 0.15F, Ease.OutQuad))
                .Chain(Tween.Scale(_rectTransform, 1, 0.15F, Ease.InQuad));
            }
           
        }
    }
}
