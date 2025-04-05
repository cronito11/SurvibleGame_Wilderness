using System;
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

    private void OnEnable ()
    {
        PlayerInventory.OnInventoryChanged += UpdateUI;
        //InventorySlot.OnItemDropOnSlot += UpdateUI;
        PlayerInventory.OnInventoryClear += ClearAllUiSlots;    
    }

    private void OnDisable ()
    {
        PlayerInventory.OnInventoryChanged -= UpdateUI;
        //InventorySlot.OnItemDropOnSlot -= UpdateUI;
        PlayerInventory.OnInventoryClear -= ClearAllUiSlots;
    }

    private void Start ()
    {
        inventorySlotsClothing = clothingItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsMaterial = materialItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsWeapon = weaponItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
        inventorySlotsFood = foodItemsGrid.GetComponentsInChildren<InventorySlot>().ToList();
    }

    void UpdateUI (InventoryItem inventoryItem)
    {
        Debug.Log($"Inventory Ui Updated {playerInventory}", gameObject);
        if (playerInventory == null || playerInventory.GetInventory() == null) //TODO: Remove this check
            return;
        // Populate UI with inventory items
        Dictionary<int, InventoryItem> inventory = playerInventory.GetInventory();

        switch(inventoryItem.gameItem.itemType)
        {
            case ItemType.Clothing:
                UpdateClothingSlots(inventory);
                break;
            case ItemType.Material:
                UpdateMaterialSlots(inventory);
                break;
            case ItemType.Food:
                UpdateFoodSlots(inventory);
                break;
            case ItemType.Weapon:
                UpdateWeaponSlots(inventory);
                break;
        }
    }

    private void ClearAllUiSlots()
    {
        ClearFoodItemUiSlots();
        ClearMaterialItemUiSlots();
        ClearWeaponItemUiSlots();
        ClearClothingItemUiSlots();
    }

    private void ClearClothingItemUiSlots()
    {
        foreach (InventorySlot child in inventorySlotsClothing)
        {
            //child.GetComponent<InventorySlot>().ClearSlot();
            child.EmptySlot();
            //Destroy(child.GetComponentInChildren<DraggableItem>().gameObject);
        }
    }

    private void ClearWeaponItemUiSlots()
    {
        foreach (InventorySlot child in inventorySlotsWeapon)
        {
            //child.GetComponent<InventorySlot>().ClearSlot();
            child.EmptySlot();
            //Destroy(child.GetComponentInChildren<DraggableItem>().gameObject);
        }
    }

    private void ClearMaterialItemUiSlots()
    {
        foreach (InventorySlot child in inventorySlotsMaterial)
        {
            //child.GetComponent<InventorySlot>().ClearSlot();
            child.EmptySlot();
            //Destroy(child.GetComponentInChildren<DraggableItem>().gameObject);
        }
    }

    private void ClearFoodItemUiSlots()
    {
        foreach (InventorySlot child in inventorySlotsFood)
        {
            //child.GetComponent<InventorySlot>().ClearSlot();
            child.EmptySlot();
            //Destroy(child.GetComponentInChildren<DraggableItem>().gameObject);  
        }
    }

    private void UpdateWeaponSlots(Dictionary<int, InventoryItem> inventory)
    {
        #region UpdateWeaponSlots
        //fetch the weapons from the inventory
        Dictionary<int, InventoryItem> weaponsInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Weapon)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        ClearWeaponItemUiSlots();
        for (int i = 0; i < weaponsInventory.Count; i++)
        {
            int j = 0;
            InventoryItem item = weaponsInventory.Values.ElementAt(i);

            //No because these items are not stackable
                //if item is already assigned to a slot then just update the text and then continue
                //if (item.isAssignedToSlot)
                //{
                //    //update the text and continue
                //    //find the postion where the item is assigned
                //    while (inventorySlotsWeapon[j].currentInventoryItem != item)
                //    {
                //        j++;
                //    }
                //    inventorySlotsWeapon[j].SetItem(item);
                //    continue;
                //}

            //if the item is not stackable then for full quantity we need to assign the item to separate slots  
            int itemCount = item.quantity;
            item.quantity = 1; // Set the quantity to 1 for each slot   
            while (itemCount > 0)
            {
                //find the first empty slot and set the item
                while (!inventorySlotsWeapon[j].isEmpty)
                {

                    if (j > inventorySlotsWeapon.Count)
                    {
                        Debug.Log("No Empty Slots");
                    }
                    j++;
                }
                inventorySlotsWeapon[j].SetItem(item);
                itemCount--;    
            }

            
        }
        #endregion
    }

    private void UpdateFoodSlots(Dictionary<int, InventoryItem> inventory)
    {
        #region UpdateFoodSlots
        //fetch the foods from the inventory
        Dictionary<int, InventoryItem> foodInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Food)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        if(foodInventory.Count == 0)
        {
            Debug.Log("No Food Items");
            ClearAllUiSlots();  
            return;
        }

        for (int i = 0; i < foodInventory.Count; i++)
        {
            int j = 0;
            InventoryItem item = foodInventory.Values.ElementAt(i);

            //if item is already assigned to a slot then just update the text and then continue
            if (item.isAssignedToSlot)
            {
                //update the text and continue
                //find the postion where the item is assigned
                while (inventorySlotsFood[j].currentInventoryItem != item)
                {
                    j++;
                }
                inventorySlotsFood[j].SetItem(item);
                continue;
            }

            //find the first empty slot and set the item
            while (!inventorySlotsFood[j].isEmpty)
            {

                if (j > inventorySlotsFood.Count)
                {
                    Debug.Log("No Empty Slots");
                }
                j++;
            }
            inventorySlotsFood[j].SetItem(item);
        }
        #endregion
    }

    private void UpdateMaterialSlots(Dictionary<int, InventoryItem> inventory)
    {
        #region UpdateMaterialSlots
        //fetch the materials from the inventory
        Dictionary<int, InventoryItem> materialsInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Material)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        for (int i = 0; i < materialsInventory.Count; i++)
        {
            int j = 0;
            InventoryItem item = materialsInventory.Values.ElementAt(i);

            //if item is already assigned to a slot then just update the text and then continue
            if (item.isAssignedToSlot)
            {
                //update the text and continue
                //find the postion where the item is assigned
                Debug.Log(inventorySlotsMaterial.Count);
                while (inventorySlotsMaterial[j].currentInventoryItem != item)
                {
                    j++;
                    Debug.Log("Slot: " + j);
                }
                inventorySlotsMaterial[j].SetItem(item);
                continue;
            }

            //find the first empty slot and set the item
            while (!inventorySlotsMaterial[j].isEmpty)
            {

                if (j > inventorySlotsMaterial.Count)
                {
                    Debug.Log("No Empty Slots");
                }
                j++;
            }
            inventorySlotsMaterial[j].SetItem(item);
        }
        #endregion
    }

    private void UpdateClothingSlots(Dictionary<int, InventoryItem> inventory)
    {
        #region UpdateClothingSlots
        Dictionary<int, InventoryItem> clothsInventory = inventory
            .Where(pair => pair.Value.gameItem.itemType == ItemType.Clothing)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
        ClearClothingItemUiSlots();
        for (int i = 0; i < clothsInventory.Count; i++)
        {
            int j = 0;
            InventoryItem item = clothsInventory.Values.ElementAt(i);

            //No because these items are not stackable
                ////if item is already assigned to a slot then just update the text and then continue
                //if (item.isAssignedToSlot)
                //{
                //    //update the text and continue
                //    //find the postion where the item is assigned
                //    while (inventorySlotsClothing[j].currentInventoryItem != item)
                //    {
                //        j++;
                //    }
                //    inventorySlotsClothing[j].SetItem(item);
                //    continue;
                //}

            int itemCount = item.quantity;
            item.quantity = 1; // Set the quantity to 1 for each slot
            while (itemCount > 0)
            {
                //find the first empty slot and set the item
                while (!inventorySlotsClothing[j].isEmpty)
                {
                    if (j > inventorySlotsClothing.Count)
                    {
                        Debug.Log("No Empty Slots");
                    }
                    j++;
                }
                inventorySlotsClothing[j].SetItem(item);
                itemCount--;
            }
        }
        #endregion
    }

}
