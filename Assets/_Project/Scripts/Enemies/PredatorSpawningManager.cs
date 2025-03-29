using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    public class PredatorSpawningManager : SpawningManager
    {
        private PassiveAnimalSpawner passiveAnimalSpawner;

        virtual protected void Start ()
        {
            passiveAnimalSpawner = FindFirstObjectByType<PassiveAnimalSpawner>();
        }

        private void OnEnable ()
        {
            TimeController.OnChangeTimeOfDay += ManageSpawningSystem;
        }

        private void OnDisable ()
        {
            TimeController.OnChangeTimeOfDay -= ManageSpawningSystem;
        }

        override protected int MaxAmount ()
        {
            int passiveAnimalCount = passiveAnimalSpawner.activeElementCount; // Count passive animals
            return passiveAnimalCount / 2; // Predator cap (optional)
        }

        override protected float GetProbability ()
        {
            return 1 - Mathf.Exp(-lambda * passiveAnimalSpawner.activeElementCount); // Exponential formula
        }

        public void ManageSpawningSystem (TimeOfDay timeOfDay)
        {
            if (timeOfDay == TimeOfDay.Night)
            {
                StartCoroutine(SpawnPrefab2());
            } else
            {
                StopAllCoroutines();
            }
        }
    }
}
