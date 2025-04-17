

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Surviblewilderness
{
    /*
     * This script will be assigned to a centralized manager object 
     * becuase only one instance should exist of this manager
     * all the predator animals will register themselves through 
     * their own managers script 
     */
    public class PredatorSaveManager : MonoBehaviour, ISaveable
    {
        private const string PREDATOR_SAVE_FILE_NAME = "PredatorsData.json";

        private static Dictionary<GameObject,Transform> allPredators = new Dictionary<GameObject, Transform>();

        [SerializeField] private GameObject predatorPrefab;

        private void OnEnable()
        {
            GameSaveManager.OnSave += Save;
            GameSaveManager.OnLoad += Load; 
        }
        private void OnDisable()
        {
            GameSaveManager.OnSave -= Save;
            GameSaveManager.OnLoad -= Load;
        }

        public static void RegisterPredator(Transform transform)
        {
            allPredators.Add(transform.gameObject,transform);
        }
        public static void UnregisterPredator(Transform transform)
        {
            if(allPredators.ContainsKey(transform.gameObject))
                allPredators.Remove(transform.gameObject);
        }

        public void Load()
        {
            //clearing the previous data of predators
            //int count = allPredators.Count;
            //while(count > 0)
            //{
            //    count--;
            //    Destroy(allPredators[allPredators.Keys.GetEnumerator().Current].gameObject);
            //}

            foreach (var key in allPredators.Keys.ToList()) // ToList() to avoid modifying the dictionary while iterating
            {
                //Transform transform = allPredators[key];
                //allPredators.Remove(key);
                //Destroy(transform.gameObject);

                if (allPredators.TryGetValue(key, out Transform transform) && transform != null)
                {
                    if (transform.gameObject.scene.IsValid()) // Avoid destroying prefabs
                    {
                        Destroy(transform.gameObject);
                    }
                    else
                    {
                        UnityEngine.Debug.LogWarning($"Skipping prefab: {transform.gameObject.name}");
                    }
                    UnityEngine.Debug.Log($"Removed predator with key: {key}");
                }

                //UnityEngine.Debug.Log($"Removed item with key: {key}");
            }

            

            //load the predators data from the save file
            PredatorSaveDataList predatorSaveDataList = SaveSystem.Load<PredatorSaveDataList>(PREDATOR_SAVE_FILE_NAME);

            foreach (PredatorSaveData predatorData in predatorSaveDataList.predatorDataList)
            {
                //instantiate the predator at saved position
                GameObject predator = GameObject.Instantiate(predatorPrefab, new Vector3(predatorData.entityPosition.x, predatorData.entityPosition.y, predatorData.entityPosition.z), Quaternion.identity);

                //reload the health of the predator
                predator.GetComponent<CharacterLifeController>().ReloadHealth((int)predatorData.predatorStats.health) ;
                
            }

        }

        public void Save()
        {
            List<PredatorSaveData> predatorDataList = new List<PredatorSaveData>();

            foreach (var predator in allPredators)
            { 
                PredatorSaveData data = new PredatorSaveData();

                data.predatorStats.health = predator.Value.GetComponent<CharacterLifeController>().health;

                data.entityPosition.x = predator.Value.transform.position.x;
                data.entityPosition.y = predator.Value.transform.position.y;
                data.entityPosition.z = predator.Value.transform.position.z;

                predatorDataList.Add(data);
                Debug.Log(JsonUtility.ToJson(predatorDataList, true).ToString());
            }

            Debug.Log(predatorDataList.Count);
            //PredatorSaveDataList predatorSaveDataList = new PredatorSaveDataList();
            // predatorSaveDataList.predatorDataList = predatorDataList;

            SaveSystem.Save(predatorDataList, PREDATOR_SAVE_FILE_NAME);
        }
        
    }

    [System.Serializable]
    public class PredatorSaveData : EntityData
    {
        public PredatorSats predatorStats;
    }

    [System.Serializable]
    public class PredatorSats : EntityStats
    {
        public PredatorSats() : base()
        {

        }
    }

    [System.Serializable]
    public class PredatorSaveDataList
    {
        public List<PredatorSaveData> predatorDataList;

       
    }
}
