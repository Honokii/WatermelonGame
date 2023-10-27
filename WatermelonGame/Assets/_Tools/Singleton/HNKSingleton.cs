using UnityEngine;

namespace HNK.Tools {
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {
        private static T _instance;

        [SerializeField] private bool dontDestroyOnLoad;

        public static T Instance {
            get {
                if (_instance == null) {
                    Debug.LogError("An Instance of " + typeof(T) + " is missing!");
                }
                return _instance;
            }
        }

        protected void Awake() {
            if (_instance == null) {
                _instance = this as T;
                if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this) Destroy(this);
        }
    }
}
