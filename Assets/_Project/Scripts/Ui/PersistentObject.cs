using UnityEngine;
using UnityEngine.SceneManagement;

namespace Surviblewilderness
{
    public class PersistentObject : MonoBehaviour
    {
        private static PersistentObject instance;

        // Check if an instance already exists
        private void Awake()
        {
            // If no instance, make this the persistent instance
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // If an instance already exists, destroy the duplicate
                Destroy(gameObject);
            }
        }
    }
}
