using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    
   
    //manages the player save data
    //saveing and loading data for player object 
    //in future it will take care of inventory as well 
    public class PlayerSaveManager : MonoBehaviour,ISaveable
    {
        //list of collectable items of the game reference from inspector
        [SerializeField] private GameItemSO[] collectableItems;

        //a dictionary to store the collectable items for faster access
        Dictionary<int, GameItemSO> collectableItemsDict = new Dictionary<int, GameItemSO>();

        [SerializeField] private const string PLAYER_SAVE_FILE_NAME = "PlayerData.json";

        private CharacterLifeController characterLifeController;
        private PlayerInventory inventory;
        private void Awake()
        {
            characterLifeController = GetComponent<CharacterLifeController>();
            inventory = GetComponent<PlayerInventory>();    
        }

        private void Start()
        {
            foreach (GameItemSO item in collectableItems)
            {
                collectableItemsDict.Add(item.id, item);
            }
        }
        private void OnEnable()
        {
            GameSaveManager.OnSave += Save;
            GameSaveManager.OnLoad += Load;
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
                    inventoryItem.Value.isAssignedToSlot
                    ));
            }
            //test purpose 
            //playerData.inventoryDataList.list.Add(new PlayerInventoryData(501, 10, 100, true));
            SaveSystem.Save(playerData, PLAYER_SAVE_FILE_NAME);
            

        }

        public void Load()
        {
            PlayerData playerData = SaveSystem.Load<PlayerData>(PLAYER_SAVE_FILE_NAME);
                        
            //reload player inventory 
            List<InventoryItem> inventoryItems = new List<InventoryItem>();

            inventory.ClearInventory(); 
            foreach(PlayerInventoryData data in playerData.inventoryDataList.list)
            {
                inventoryItems.Add(new InventoryItem(collectableItemsDict[data.itemId], data.quantity, data.isAssignedToSlot));
            }
            inventory.ReloadInventory(inventoryItems);

            //reload player position
            Vector3 loadedPosition = new Vector3(playerData.entityPosition.x, playerData.entityPosition.y, playerData.entityPosition.z);
            transform.position = loadedPosition;
            Debug.Log($"Loaded postion {loadedPosition}");

            //realod player stats   
            characterLifeController.ReloadHealth((int)playerData.playerStats.health);
            /*
             * Hubger and thirst will be implemented in future
             */
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
        public bool isAssignedToSlot;

        public PlayerInventoryData(int itemId, int quantity, bool isAssignedToSlot)
        {
            this.itemId = itemId;
            this.quantity = quantity;
            this.isAssignedToSlot = isAssignedToSlot;
        }
    }
}
