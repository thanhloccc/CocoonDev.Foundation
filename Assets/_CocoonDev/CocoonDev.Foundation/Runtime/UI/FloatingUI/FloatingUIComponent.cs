using Cysharp.Text;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CocoonDev.Foundation
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class FloatingUIComponent : MonoBehaviour
    {
        [Title("Component Refs", titleAlignment: TitleAlignments.Centered)]
        [SerializeField, Required]
        private RectTransform _rectTransform;
        [SerializeField, Required]
        private CanvasGroup _canvasGroup;

        [SerializeField, Required]
        private Image _icon;
        [SerializeField, Required]
        private TextMeshProUGUI _valueText;

        public void Initialize( FloatingOptions options)
        {
            InitializeAndForget( options).Forget();
        }

        public async UniTaskVoid InitializeAndForget( FloatingOptions options)
        {
            await InitializeAsync(options);
        }

        public async UniTask InitializeAsync(FloatingOptions options)
        {
            await FloatingUIAnimateHelper.FloatingAsync(_rectTransform
                , _canvasGroup
                , options);
        }

        public void SetUIData(FloatingUIData data)
        {
            // ZString
            using (var sb = ZString.CreateStringBuilder())
            {
                sb.Append('+');
                sb.Append(Mathf.CeilToInt(data.Value));
                _valueText.TrySetText(sb);
            }

            _icon.sprite = data.Icon;
            _icon.SetNativeSize();
        }

        public void SetPosition(Vector2 originPosition)
        {
            _rectTransform.position = originPosition;
        }

        public void OnReturnToPool()
        {
            _rectTransform.position = Vector2.zero;
            _canvasGroup.alpha = 1;
        }

        
    }
}
