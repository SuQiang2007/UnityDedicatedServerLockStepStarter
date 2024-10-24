using UnityEngine;

namespace Client.ClientUtils
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                    if (_instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        _instance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }

        public void DestroySelf()
        {
            Dispose();
            MonoSingleton<T>._instance = null;
            UnityEngine.Object.Destroy(gameObject);
        }

        protected virtual void Dispose()
        {

        }
    }
}