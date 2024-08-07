using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

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
