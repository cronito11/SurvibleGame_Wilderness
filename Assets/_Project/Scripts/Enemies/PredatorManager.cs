using UnityEngine;

namespace Surviblewilderness
{
    public class PredatorManager : MonoBehaviour
    {

        private void Awake()
        {
            //registering this predator into a static list of predators for save 
            PredatorSaveManager.RegisterPredator(this.transform);
        }

        private void OnDestroy()
        {
            //unregister/remove this predator from the static list of predator  
            PredatorSaveManager.UnregisterPredator(this.transform); 
        }
    }
}
