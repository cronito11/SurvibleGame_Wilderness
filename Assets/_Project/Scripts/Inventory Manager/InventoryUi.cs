using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] private Transform clothingItemsGrid;  // Grid where slots will be placed
    [SerializeField] private Transform materialItemsGrid;  // Grid where slots will be placed
    [SerializeField] private Transform weaponItemsGrid;  // Grid where slots will be placed
    [SerializeField] private Transform foodItemsGrid;  // Grid where slots will be placed
    
    [SerializeField] private GameObject slotPrefab;
    

    [SerializeField] private PlayerInventory playerInventory;

    [SerializeField] List<InventorySlot> inventorySlotsClothing = new List<InventorySlot>();
    [SerializeField] List<InventorySlot> inventorySlotsMaterial = new List<InventorySlot>();
    [SerializeField] List<InventorySlot> inventorySlotsWeapon = new List<InventorySlot>();
    [SerializeField] List<InventorySlot> inventorySlotsFood = new List<InventorySlot>();

    private void OnEnable()
    {
        PlayerInventory.OnInventoryChanged += UpdateUI;
        //InventorySlot.OnItemDropOnSlot += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerInventory.OnInventoryChanged -= UpdateUI;
        //InventorySlot.OnItemDropOnSlot -= UpdateUI;
    }

    private void Start()
    {
        inventorySlotsClothing = clothingItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsMaterial = materialItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsWeapon = weaponItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsFood = foodItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
    }

    void UpdateUI(InventoryItem inventoryItem)
    {
        Debug.Log("Inventory Ui Updated");
        // Populate UI with inventory items
        Dictionary<int, InventoryItem> inventory = playerInventory.GetInventory();
        
        Dictionary<int, InventoryItem> clothsInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Clothing)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        Dictionary<int, InventoryItem> materialsInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Material)
            .ToDictionary(pair => pair.Key, pair => pair.Value);


        Dictionary<int, InventoryItem> foodInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Food)
            .ToDictionary(pair => pair.Key, pair => pair.Value);    

        Dictionary<int, InventoryItem> weaponsInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Weapon)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        
        
        for(int i = 0; i < inventorySlotsClothing.Count; i++)
        {
            if(i<clothsInventory.Count)
            {
                InventoryItem item = clothsInventory.Values.ElementAt(i);

                if (item.isAssignedToSlot)
                    continue;
                inventorySlotsClothing[i].SetItem(item);
            }
            
        }

        for (int i = 0; i < inventorySlotsMaterial.Count; i++)
        {

            if (i < materialsInventory.Count)
            {
                InventoryItem item = materialsInventory.Values.ElementAt(i);

                if (item.isAssignedToSlot)
                    continue;
                inventorySlotsMaterial[i].SetItem(item);
            }
           
        }

        for(int i = 0,j=0; i < foodInventory.Count; i++)
        {
            InventoryItem item = foodInventory.Values.ElementAt(i);
            if (item.isAssignedToSlot)
                continue;

            while (!inventorySlotsFood[j].isEmpty)
            {
                if(inventorySlotsFood[j].currentInventoryItem.gameItem.id == item.gameItem.id)
                {
                    break;
                    //inventorySlotsFood[j].SetItem(item);
                }
                j++;
                if (j > inventorySlotsFood.Count)
                {
                    Debug.Log("No Empty Slots");
                }
            }
            inventorySlotsFood[j].SetItem(item);
        }

        //for (int i = 0, j=0; i < inventorySlotsFood.Count; i++)
        //{

        //    if (j < foodInventory.Count)
        //    {
        //        InventoryItem item = foodInventory.Values.ElementAt(i); 
                
        //        if(item.isAssignedToSlot)
        //            continue;

        //        inventorySlotsFood[i].SetItem(item);
        //        Debug.Log("Setting food item"+ foodInventory.Values.ElementAt(i).gameItem.name);
        //    }
        //}

        for (int i = 0; i < inventorySlotsWeapon.Count; i++)
        {
            if (i < weaponsInventory.Count)
            {
                InventoryItem item = weaponsInventory.Values.ElementAt(i);
                if (item.isAssignedToSlot)
                    continue;
                inventorySlotsWeapon[i].SetItem(item);
            }
        }

    }
}
