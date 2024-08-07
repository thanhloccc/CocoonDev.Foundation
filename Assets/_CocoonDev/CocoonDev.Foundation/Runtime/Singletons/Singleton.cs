using UnityEngine;

namespace CocoonDev.Foundation
{
    public class Singleton<T> : MonoBehaviour
                where T : Component
    {
        protected static T s_instance;

        public static bool HasInstance => s_instance != null;
        public static T TryGetInstance() => HasInstance ? s_instance : null;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = FindAnyObjectByType<T>();
                    if (s_instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        s_instance = go.AddComponent<T>();
                    }
                }

                return s_instance;
            }
        }

        /// <summary>
        /// Make sure to call base.Awake() in override if you need awake.
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying)
                return;

            s_instance = this as T;
        }
    }
}
