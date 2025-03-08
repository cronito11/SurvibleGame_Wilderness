using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
   
    //manages the player save data
    //saveing and loading data for player object 
    //in future it will take care of inventory as well 
    public class PlayerSaveManager : MonoBehaviour,ISaveable
    {
        [SerializeField] private const string PLAYER_SAVE_FILE_NAME = "PlayerData.json";

        private CharacterLifeController characterLifeController;
        private PlayerInventory inventory;
        private void Awake()
        {
            characterLifeController = GetComponent<CharacterLifeController>();
            inventory = GetComponent<PlayerInventory>();    
        }

        private void OnEnable()
        {
            GameSaveManager.OnSave += Save;
        }

        public void Save()
        {
            PlayerData playerData = new PlayerData();

            playerData.entityPosition.x = transform.position.x;
            playerData.entityPosition.y = transform.position.y;
            
            playerData.entityPosition.z = transform.position.z; 


            playerData.playerStats.health = characterLifeController.health;
            playerData.playerStats.hunger = 0;
            playerData.playerStats.thirst = 0;


            //inventory 
            foreach (var inventoryItem in inventory.GetInventory())
            {
                playerData.inventoryDataList.list.Add(new PlayerInventoryData(
                    inventoryItem.Value.gameItem.id, 
                    inventoryItem.Value.quantity, 
                    inventoryItem.Value.currentDurability, 
                    inventoryItem.Value.isAssignedToSlot
                    ));
            }
            //test purpose 
            //playerData.inventoryDataList.list.Add(new PlayerInventoryData(501, 10, 100, true));
            SaveSystem.Save(playerData, PLAYER_SAVE_FILE_NAME);
            

        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }
    }
    //JSON architecture for playerdata 
    [System.Serializable]
    public class PlayerData : EntityData
    {
        public PlayerStats playerStats;
        public PlayerInventoryDataList inventoryDataList;
        public PlayerData(): base() 
        {
            playerStats = new PlayerStats(); 
            inventoryDataList = new PlayerInventoryDataList();  
        }
    }

    [System.Serializable]
    public class PlayerStats : EntityStats
    {
        public float hunger;
        public float thirst;

        public PlayerStats() : base()
        {

        }
    }

    [System.Serializable]

    public class PlayerInventoryDataList
    {
        public List<PlayerInventoryData> list;

        public PlayerInventoryDataList()
        {
            list = new List<PlayerInventoryData>();
        }


    }

    [System.Serializable]
    public class PlayerInventoryData
    {
        public int itemId;
        public int quantity;
        public int currentDurability;
        public bool isAssignedToSlot;

        public PlayerInventoryData(int itemId, int quantity, int currentDurability, bool isAssignedToSlot)
        {
            this.itemId = itemId;
            this.quantity = quantity;
            this.currentDurability = currentDurability;
            this.isAssignedToSlot = isAssignedToSlot;
        }
    }
}
