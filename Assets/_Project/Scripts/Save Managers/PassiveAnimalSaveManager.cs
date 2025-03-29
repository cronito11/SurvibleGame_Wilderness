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

            foreach (PassiveAimaalData predatorData in passiveAnimalDataList.list)
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

            //NOTE:: complete this loop when the architecture of passive animal is complete DISCUSSED WITH LUIS
            //foreach (var passiveAnimal in allPassiveAnimals)
            //{
            //    passiveAnimalDataList.list.Add(new PassiveAimaalData())
            //}

            //just for testing the architecture is not ready yet
            //passiveAnimalDataList.list.Add(new PassiveAimaalData(false, false, 0f, 30, false, 0f));

            SaveSystem.Save(passiveAnimalDataList, PASSIVE_ANIMAL_SAVE_FILE_NAME);
        }
    }

    [System.Serializable]
    public class PassiveAimaalData : EntityData
    {
        public PassiveAnimalStats passiveAnimalStats;
        public PassiveAimaalData(bool isMale, bool isPregnant, float pregnancyTimeLeft, int offSpringCount, bool isBaby, float growthTimeLeft) : base()
        {
            passiveAnimalStats = 
                new PassiveAnimalStats(isMale,isPregnant,pregnancyTimeLeft,offSpringCount,isBaby,growthTimeLeft);
        }
    }

    [System.Serializable]
    public class PassiveAnimalStats : EntityStats
    {
        public bool isMale;
        public bool isPregnant;
        public float pregnancyTImeLeft;
        public int offSpringCount;  //number of children produced by that nimal BODY COUNT indirectly.

        public bool isBaby;
        public float growthTimeLeft;

        /*booleans are used in place of custom enums as of 
         * now becuase the breeding mechanic and passive animal are 
         * yet to define.              
         */

        public PassiveAnimalStats() : base()
        {

        }

        public PassiveAnimalStats(bool isMale, bool isPregnant, float pregnancyTimeLeft, int offSpringCount, bool isBaby, float growthTimeLeft) : base()
        {
            this.isMale = isMale;
            this.isPregnant = isPregnant;
            this.pregnancyTImeLeft = pregnancyTimeLeft; 
            this.offSpringCount = offSpringCount;   
            this.isBaby = isBaby;   
            this.growthTimeLeft = growthTimeLeft;   
        }
    }

    [System.Serializable]
    public class PassiveAnimalDataList
    {
        public List<PassiveAimaalData> list;

        public PassiveAnimalDataList()
        {
            list = new List<PassiveAimaalData>();
        }
    }
}
