using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class GameSaveManager : MonoBehaviour
    {

        //this event will be triggered when player clicks on save buttom from ui 
        //also this will we subscribed by evey savable entities players, enemies, environmental object etc
        //if this event is triggered then those individual entity who subcribed will save it self to their saperate json object.
        public static event Action OnSave;

        void Update()
        {
            //for test purpose 
            if (Input.GetKeyDown(KeyCode.P)) 
            { 
                SaveGame();
            }
        }

        //this method will be assigned to save button in the ui 
        public void SaveGame()
        {

            OnSave?.Invoke();
            Debug.Log("Game Saved");
        }

    }
}
