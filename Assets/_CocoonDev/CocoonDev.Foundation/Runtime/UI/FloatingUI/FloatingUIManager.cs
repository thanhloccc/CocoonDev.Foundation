using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using ZBase.Foundation.Pooling.UnityPools;

namespace CocoonDev.Foundation
{
    public class FloatingUIManager : MonoBehaviour
    {
        private static FloatingUIManager s_instance;

        [Title("Pooler", titleAlignment: TitleAlignments.Centered)]
        [SerializeField]
        private bool _prepoolOnStart;
        [SerializeField]
        private int _prepoolAmount;
        [SerializeField, Required]
        private FloatingUIComponent _sourcePool;
        [SerializeField, Required]
        private Transform _parentPool;

        private bool _initialized;
        private bool _initializedPool;

        private static ComponentPool<FloatingUIComponent, ComponentPrefab<FloatingUIComponent>> s_pool;
        private static CancellationTokenSource s_loadingCts;

        #region Unity Methods
        private void Awake()
        {
            if (_initialized)
                return;

            s_instance = this;
            _initialized = true;
        }

        private async void Start()
        {
            if (_initializedPool)
                return;

            await InitializePoolAsync();
            _initializedPool = true;
        }

        private void OnDestroy()
        {
            s_pool.ReleaseInstances(0);
            s_pool.Dispose();
        }
        #endregion

        private async UniTask InitializePoolAsync()
        {
            s_pool = new(new ComponentPrefab<FloatingUIComponent> {
                Parent = _parentPool,
                PrepoolAmount = _prepoolAmount,
                Source = _sourcePool,
            });

            if(_prepoolOnStart)
                await s_pool.Prepool(this.GetCancellationTokenOnDestroy());
        }

        public static void Push(Vector2 originPosition, FloatingOptions options, FloatingUIData data)
        {
            PushAndForget(originPosition, options, data).Forget();
        }

        public static async UniTaskVoid PushAndForget(Vector2 originPosition
            , FloatingOptions options
            , FloatingUIData data)
        {
            await PushAsync(originPosition, options, data);
        }

        public static async UniTask PushAsync(Vector2 originPosition
            , FloatingOptions options
            , FloatingUIData data)
        {
            var instance = await s_pool.Rent();
            instance.SetPosition(originPosition);
            instance.gameObject.SetActive(true);
          
            await instance.InitializeAsync(options);
            s_instance.ReturnToPool(instance);
        }

        private void ReturnToPool(FloatingUIComponent instance)
        {
            instance.OnReturnToPool();
            s_pool.Return(instance);
        }

    }
}
