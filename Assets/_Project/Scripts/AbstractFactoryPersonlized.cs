using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    public abstract class AbstractFactoryPersonlized : MonoBehaviour
    {
        public GameObject prefab;

        [SerializeField] private int capacity = 10;
        [SerializeField] private float spawnRate = 5f;
        [SerializeField] protected float lambda = 0.1f; // Exponential distribution parameter 
        [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

        #region KmeansClusteringAlgorithm
        [Header("K-means clustering alorithm (not functional yet)")]
        [SerializeField] private int kClusters = 3; // Number of clusters
        [SerializeField] private float spawnRadius = 5f;
        #endregion

        public int activeElementCount { get; private set; }

        protected IEnumerator SpawningPrefab ()
        {
            if (activeElementCount < capacity)
            {
                for (int i = 0; i < capacity - activeElementCount; i++)
                {
                    /* 
                     * In future , we can use K-means clustering algorithm to spawn predators in different locations
                     */

                    int index = Random.Range(0, spawnPoints.Count);
                    Vector3 spawnPosition = spawnPoints[index].position;

                    GameObject instnace = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
                    activeElementCount++;
                    yield return new WaitForSecondsRealtime(spawnRate);
                }
            }
        }

        abstract protected int MaxAmount ();
        abstract protected float GetProbability ();

        protected void SpawnPrefab2 ()
        {
            for(int idx = 0; idx < MaxAmount(); idx++)
            {
                SpawnPrefab();
            }
        }

        protected void SpawnPrefab ()
        {
            Vector3 spawnPosition = GetRandomSpawnLocation();
            GameObject predator = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
            activeElementCount++;
        }

        protected Vector3 GetRandomSpawnLocation ()
        {
            // Implement your logic for spawning (e.g., around passive animals)

            int index = Random.Range(0, spawnPoints.Count);
            Vector3 spawnPosition = spawnPoints[index].position;
            return spawnPosition;
        }
    }
}
