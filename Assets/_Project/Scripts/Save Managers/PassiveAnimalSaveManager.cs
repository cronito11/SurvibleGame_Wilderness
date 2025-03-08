using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Surviblewilderness
{
    public class PassiveAnimalSaveManager : MonoBehaviour, ISaveable
    {
        private static Dictionary<GameObject, Transform> allPassiveAnimals = new Dictionary<GameObject, Transform>();

        private const string PREDATOR_SAVE_FILE_NAME = "PassiveAnimalsData.json";

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
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            //get the list of all passive animals 
            //loop through the list and save all the passive animal data into a list 
            //pass that list to save system and save it 
            PassiveAnimalDataList passiveAnimalDataList = new PassiveAnimalDataList();

            //NOTE:: complete this loop when the architecture of passive animal is complete DISCUSS WITH LUIS
            //foreach (var passiveAnimal in allPassiveAnimals)
            //{
            //    passiveAnimalDataList.list.Add(new PassiveAimaalData())
            //}

            //just for testing the architecture is not ready yet
            passiveAnimalDataList.list.Add(new PassiveAimaalData(false, false, 0f, 30, false, 0f));

            SaveSystem.Save(passiveAnimalDataList, PREDATOR_SAVE_FILE_NAME);
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
