using UnityEngine;
using UnityEngine.SceneManagement;

namespace Surviblewilderness
{
    public class PersistentUI : MonoBehaviour
    {
        private static PersistentUI instance;

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
