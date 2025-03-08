using UnityEngine;
using UnityEngine.SceneManagement;

namespace Surviblewilderness
{
    public class PersistentObject : MonoBehaviour
    {
        private static PersistentObject instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Keep the UI across scenes
            }
            else
            {
                Destroy(gameObject); // Prevent duplicates
            }
        }
    }
}
