using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using Utility;

namespace Surviblewilderness
{
    public class PassiveAnimalSpawner : MonoBehaviour
    {

        [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
        [SerializeField, ReadOnly] private List<PassiveAnimalManager> passiveAnimals = new List<PassiveAnimalManager>();
        private  const int MAX_AMOUNT = 1000000;
        private int initialQuantity = 10;
        private bool nextIsMale = true;
        public GameObject prefab;


        public int activeElementCount { get; private set; }
        protected int passiveAnimalCount;

        public PassiveAnimalManager GetRandomPassiveAnimal ()
        {
            if (passiveAnimals.Count == 0)
                return null;
            int index = Random.Range(0, passiveAnimals.Count);
            PassiveAnimalManager animal = passiveAnimals[index];
            return animal;
        }
        public void InstantiateChild ()
        {
            SpawnPrefab(true);
        }

        protected void SpawnPrefab (bool isChild)
        {
            Vector3 spawnPosition = GetRandomSpawnLocation();
            PassiveAnimalManager animal = Instantiate(prefab, spawnPosition, Quaternion.identity, transform).GetComponent<PassiveAnimalManager>();
            passiveAnimals.Add(animal);
            animal.ResetStats(nextIsMale, isChild ? PassiveAnimalState.Growing : PassiveAnimalState.None);
            nextIsMale = !nextIsMale;
            activeElementCount++;
        }

        public void RemovePassiveAnimal (PassiveAnimalManager animal)
        {
            if (!passiveAnimals.Contains(animal))
                return;
           
            passiveAnimals.Remove(animal);
            activeElementCount--;
        }

        protected Vector3 GetRandomSpawnLocation ()
        {
            int index = Random.Range(0, spawnPoints.Count);
            Vector3 spawnPosition = spawnPoints[index].position;
            return spawnPosition;
        }

        private void Start ()
        {
            for(int i = 0; i < initialQuantity; i++)
            {
                SpawnPrefab(false);
            } 
        }
    }
}
