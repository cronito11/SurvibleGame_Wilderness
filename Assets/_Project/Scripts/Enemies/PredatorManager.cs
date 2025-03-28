using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    public class PredatorManager : MonoBehaviour
    {
        //private static Dictionary <GameObject, Transform> currentActivePredator = new Dictionary<GameObject, Transform>();
        private void Awake()
        {
            //registering this predator into a static list of predators for save 
            PredatorSaveManager.RegisterPredator(this.transform);
            //currentActivePredator.Add(this.gameObject, this.transform);
        }

        private void OnDestroy()
        {
            //unregister/remove this predator from the static list of predator  
            PredatorSaveManager.UnregisterPredator(this.transform);
           // currentActivePredator.Remove(this.gameObject);
        }

        //public Dictionary<GameObject, Transform> GetPredators()
        //{
        //    return currentActivePredator;
        //}   

        //public void ReloadPredators(Dictionary<>)
    }
}
