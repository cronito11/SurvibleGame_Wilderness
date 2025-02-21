using UnityEngine;

namespace Utility {
    public abstract class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T> 
    {
        private static T _instance;

        /// <summary>
        /// Provides access to the singleton instance, creating it if necessary.
        /// </summary>
        public static T Instance {
            get {
                if (_instance != null)
                    return _instance;

                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T).ToString() + " (Singleton)";

                // Make it persist across scenes
                DontDestroyOnLoad(singletonObject);               

                return _instance;
            }
        }

        protected virtual void Awake () 
        {         
            // Ensure only one instance exists
            if (_instance == null) {
                _instance = (T)this;
                DontDestroyOnLoad(gameObject);
            } else if (_instance != this) {
                Destroy(gameObject); // Destroy duplicate
            }            
        }
    }
}