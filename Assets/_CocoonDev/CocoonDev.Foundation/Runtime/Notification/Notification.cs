using Cysharp.Threading.Tasks;
using PrimeTween;
using Sirenix.OdinInspector;
using UnityEngine;
using ZBase.Foundation.Pooling.UnityPools;

namespace CocoonDev.Foundation
{
    public class Notification : MonoBehaviour
    {
        private static Notification s_instance;

        [Title("Pooler", titleAlignment: TitleAlignments.Centered)]
        [SerializeField]
        private bool _prepoolOnStart;
        [SerializeField]
        private int _prepoolAmount;
        [SerializeField]
        private NoticeComponent _sourcePool;

        private bool _initialized;
        private static bool s_isReadyShowNotice = true;

        private ComponentPool<NoticeComponent, ComponentPrefab<NoticeComponent>> _pool;

        #region Unity Methods
        private void Awake()
        {
            if (_initialized) return;

            s_instance = this;

            InitializePool();
        }

        private async void Start()
        {
            if (_prepoolOnStart)
                await _pool.Prepool(this.GetCancellationTokenOnDestroy());
        }

        private void Reset()
        {
            s_isReadyShowNotice = true;
        }

        #endregion

        private void InitializePool()
        {
            _pool = new(new ComponentPrefab<NoticeComponent> {
                Parent = transform,
                PrepoolAmount = _prepoolAmount,
                Source = _sourcePool
            });
        }

        public static void DisplayNotice(NoticeOptions options)
        {
            if (IsNoticeTimeReady())
            {
                DisplayNoticeAndForget(options).Forget();

                if (options.IgnoreWaitTime)
                    WaitForNotice(options.WaitTime).Forget();
            }
        }

        public static async UniTaskVoid DisplayNoticeAndForget(NoticeOptions options)
        {
            await DisplayNoticeAsync(options);
        }

        public static async UniTask DisplayNoticeAsync(NoticeOptions options)
        {
            var instance = await s_instance.Get();
            instance.Initialize(options.Message);
            ReturnToPool(options.Duraion, instance);
        }

        public static async UniTaskVoid WaitForNotice(float waitTime)
        {
            s_isReadyShowNotice = false;

            await UniTask.WaitForSeconds(waitTime
                , cancellationToken: s_instance.GetCancellationTokenOnDestroy());

            s_isReadyShowNotice = true;

        }

        public static bool IsNoticeTimeReady()
        {
            return s_isReadyShowNotice;
        }

        public static void ReturnToPool(float lifeTime
            , NoticeComponent instance)
        {
            Tween.Delay(lifeTime, () => s_instance._pool.Return(instance));
        }

        private async UniTask<NoticeComponent> Get()
        {
            var instance =  await _pool.Rent();
            instance.gameObject.SetActive(true);

            return instance;
        }
    }

    public readonly struct NoticeOptions
    {
        public readonly string Message;
        public readonly float Duraion;
        public readonly float WaitTime;
        public readonly bool IgnoreWaitTime;

        public NoticeOptions(string message
            , float duration = 0.75F
            , bool ignoreWaitTime = false
            , float waitTime = 0.5F)
        {
            Message = message;
            Duraion = duration;
            IgnoreWaitTime = ignoreWaitTime;
            WaitTime = waitTime;
        }
    }
}
