using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Surviblewilderness
{
    public class PassiveAnimalSaveManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private GameObject passiveAnimalPrefab;

        private static Dictionary<GameObject, Transform> allPassiveAnimals = new Dictionary<GameObject, Transform>();

        private const string PASSIVE_ANIMAL_SAVE_FILE_NAME = "PassiveAnimalsData.json";

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
            allPassiveAnimals.Add(transform.gameObject, transform);
        }
        public static void UnregisterPredator(Transform transform)
        {
            allPassiveAnimals.Remove(transform.gameObject);
        }

        public void Load()
        {
            foreach (var key in allPassiveAnimals.Keys.ToList()) // ToList() to avoid modifying the dictionary while iterating
            {
                //Transform transform = allPredators[key];
                //allPredators.Remove(key);
                //Destroy(transform.gameObject);

                if (allPassiveAnimals.TryGetValue(key, out Transform transform) && transform != null)
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
            PassiveAnimalDataList passiveAnimalDataList  = SaveSystem.Load<PassiveAnimalDataList>(PASSIVE_ANIMAL_SAVE_FILE_NAME);   

            foreach (PassiveAnimalData predatorData in passiveAnimalDataList.list)
            {
                //instantiate the predator at saved position
                GameObject predator = GameObject.Instantiate(passiveAnimalPrefab, new Vector3(predatorData.entityPosition.x, predatorData.entityPosition.y, predatorData.entityPosition.z), Quaternion.identity);

                //reload the health of the passive animal
                predator.GetComponent<CharacterLifeController>().ReloadHealth((int)predatorData.passiveAnimalStats.health);

                //reload the pregnansy stats of the passive animals

            }
        }

        public void Save()
        {
            //get the list of all passive animals 
            //loop through the list and save all the passive animal data into a list 
            //pass that list to save system and save it 
            PassiveAnimalDataList passiveAnimalDataList = new PassiveAnimalDataList();

            passiveAnimalDataList.list.Add(new PassiveAnimalData(false, PassiveAnimalState.None, 0f, 30));

            SaveSystem.Save(passiveAnimalDataList, PASSIVE_ANIMAL_SAVE_FILE_NAME);
        }
    }

    [System.Serializable]
    public class PassiveAnimalData : EntityData
    {
        public PassiveAnimalStats passiveAnimalStats;
        public PassiveAnimalData (bool isMale, PassiveAnimalState state, float timer, int offSpringCount) : base()
        {
            passiveAnimalStats = 
                new PassiveAnimalStats(isMale,state,timer,offSpringCount);
        }
    }
    public enum PassiveAnimalState
    {
        None = 0,
        Growing = 1,
        Pregnant = 2,
    }

    [System.Serializable]
    public class PassiveAnimalStats : EntityStats
    {
        public bool isMale;
        public PassiveAnimalState secundaryState;
        public float timer;
        public int offSpringCount;  //number of children produced by that nimal BODY COUNT indirectly.

        /*booleans are used in place of custom enums as of 
         * now becuase the breeding mechanic and passive animal are 
         * yet to define.              
         */

        public PassiveAnimalStats() : base()
        {

        }

        public PassiveAnimalStats(bool isMale, PassiveAnimalState state, float timer, int offSpringCount) : base()
        {
            this.isMale = isMale;
            this.secundaryState = state;
            this.offSpringCount = offSpringCount;   
            this.timer = timer;   
        }
    }

    [System.Serializable]
    public class PassiveAnimalDataList
    {
        public List<PassiveAnimalData> list;

        public PassiveAnimalDataList()
        {
            list = new List<PassiveAnimalData>();
        }
    }
}
