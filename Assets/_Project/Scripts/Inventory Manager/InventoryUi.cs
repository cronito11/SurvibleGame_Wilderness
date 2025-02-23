using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    }

    private void OnDisable()
    {
        PlayerInventory.OnInventoryChanged -= UpdateUI;
    }

    private void Start()
    {
        inventorySlotsClothing = clothingItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsMaterial = materialItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsWeapon = weaponItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsFood = foodItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
    }

    void UpdateUI()
    {
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
                inventorySlotsClothing[i].SetItem(clothsInventory.Values.ElementAt(i));
            }
            else
            {
                inventorySlotsClothing[i].ClearSlot();
            }
        }

        for (int i = 0; i < inventorySlotsMaterial.Count; i++)
        {
            if (i < materialsInventory.Count)
            {
                inventorySlotsMaterial[i].SetItem(materialsInventory.Values.ElementAt(i));
            }
            else
            {
                inventorySlotsMaterial[i].ClearSlot();
            }
        }

        for (int i = 0; i < inventorySlotsFood.Count; i++)
        {
            if (i < foodInventory.Count)
            {
                inventorySlotsFood[i].SetItem(foodInventory.Values.ElementAt(i));
            }
            else
            {
                inventorySlotsFood[i].ClearSlot();
            }
        }

        for (int i = 0; i < inventorySlotsWeapon.Count; i++)
        {
            if (i < weaponsInventory.Count)
            {
                inventorySlotsWeapon[i].SetItem(weaponsInventory.Values.ElementAt(i));
            }
            else
            {
                inventorySlotsWeapon[i].ClearSlot();
            }
        }

    }
}
