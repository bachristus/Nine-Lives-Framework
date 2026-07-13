using UnityEngine;

namespace NineLives.Framework.Unity
{    
    public class PersistantSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {            
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = gameObject.GetComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
