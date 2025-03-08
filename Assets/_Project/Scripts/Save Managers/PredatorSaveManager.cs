

using System.Collections.Generic;

using UnityEngine;

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

        private void OnEnable()
        {
            GameSaveManager.OnSave += Save; 
        }
        private void OnDisable()
        {
            GameSaveManager.OnSave -= Save;
        }

        public static void RegisterPredator(Transform transform)
        {
            allPredators.Add(transform.gameObject,transform);
        }
        public static void UnregisterPredator(Transform transform)
        {
            allPredators.Remove(transform.gameObject);
        }

        public void Load()
        {
            
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
            PredatorSaveDataList predatorSaveDataList = new PredatorSaveDataList();
            predatorSaveDataList.predatorDataList = predatorDataList;

            SaveSystem.Save(predatorSaveDataList, PREDATOR_SAVE_FILE_NAME);
        }
        
    }

    [System.Serializable]
    public class PredatorSaveData : EntityData
    {
        public PredatorSats predatorStats;
        public PredatorSaveData() : base()
        {
            predatorStats = new PredatorSats();
        }
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

        public PredatorSaveDataList() 
        {
            predatorDataList = new List<PredatorSaveData>();
        }   
    }
}
