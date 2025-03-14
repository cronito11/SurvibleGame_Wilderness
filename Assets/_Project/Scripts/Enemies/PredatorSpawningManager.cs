using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    public class PredatorSpawningManager : MonoBehaviour
    {
        public GameObject predatorPrefab;

        [SerializeField] private int predatorCap = 10;
        [SerializeField] private float spawnRate = 5f;
        [SerializeField] private float lambda = 0.1f; // Exponential distribution parameter 
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

        #region KmeansClusteringAlgorithm
        [Header("K-means clustering alorithm (not functional yet)")]        
        [SerializeField] private int kClusters = 3; // Number of clusters
        [SerializeField] private float spawnRadius = 5f;
        #endregion

        public int activePredatorsCount { get; private set; }

        private void OnEnable()
        {
            TimeController.OnChangeTimeOfDay += ManageSpawningSystem;
        }

        private void OnDisable()
        {
            TimeController.OnChangeTimeOfDay -= ManageSpawningSystem;
        }


        private void Start()
        {
            //ManageSpawningSystem(TimeOfDay.Night);
        }   
        public void ManageSpawningSystem(TimeOfDay timeOfDay)
        {
            if (timeOfDay == TimeOfDay.Night)
            {
                StartCoroutine(SpawnPredators2());
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator SpawnPredators()
        {
            if (activePredatorsCount < predatorCap)
            {
                for (int i = 0; i < predatorCap - activePredatorsCount; i++)
                {
                    /* 
                     * In future , we can use K-means clustering algorithm to spawn predators in different locations
                     */

                    int index = Random.Range(0, spawnPoints.Count);
                    Vector3 spawnPosition = spawnPoints[index].position;  
                    
                    GameObject predator = Instantiate(predatorPrefab, spawnPosition, Quaternion.identity);
                    activePredatorsCount++;
                    yield return new WaitForSecondsRealtime(spawnRate);
                }
            }
        }

        IEnumerator SpawnPredators2()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnRate);

                int passiveAnimalCount = PassiveAnimalSpawner.Instance.activePassiveAnimals; // Count passive animals
                int maxPredators = passiveAnimalCount / 2; // Predator cap (optional)

                if (activePredatorsCount < maxPredators)
                {
                    float probability = 1 - Mathf.Exp(-lambda * passiveAnimalCount); // Exponential formula
                    float randomValue = Random.Range(0f, 1f);

                    if (randomValue < probability)
                    {
                        SpawnPredator();
                    }
                }
            }
        }

        void SpawnPredator()
        {
            Vector3 spawnPosition = GetRandomSpawnLocation();
            GameObject predator = Instantiate(predatorPrefab, spawnPosition, Quaternion.identity);
            activePredatorsCount++;
        }

        Vector3 GetRandomSpawnLocation()
        {
            // Implement your logic for spawning (e.g., around passive animals)

            int index = Random.Range(0, spawnPoints.Count);
            Vector3 spawnPosition = spawnPoints[index].position;
            return spawnPosition;
        }
    }
}
