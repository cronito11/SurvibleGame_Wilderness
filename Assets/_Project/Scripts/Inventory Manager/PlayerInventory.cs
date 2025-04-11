using Surviblewilderness;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerInventory : SingletonBase<PlayerInventory>
{
    //public static PlayerInventory Instance { get; private set; }

    private Dictionary<int, InventoryItem> inventory = new Dictionary<int, InventoryItem>();

    public static event Action<InventoryItem> OnInventoryChanged;  // 🔥 Event for inventory updates
    public static event Action OnInventoryClear;  // 🔥 Event for item usage

    //private void Awake()
    //{
    //    inventory = new Dictionary<int, InventoryItem>();
    //    if (Instance == null)
    //        Instance = this;
    //    else
    //        Destroy(gameObject);
    //}

    private void Start()
    {
        //inventorySlotsClothing = 
    }

    private void OnEnable()
    {
        InteractableObject.OnItemPickedUp += AddItem;
        InventoryItem.OnItemUsed += RemoveItem; // Subscribe to item usage event
        DropMaterial.OnRemoveMaterialFromInventory += RemoveItem; // Subscribe to item drop event
    }

    private void OnDisable()
    {
        InteractableObject.OnItemPickedUp -= AddItem;
        InventoryItem.OnItemUsed -= RemoveItem; // Unsubscribe from item usage event
        DropMaterial.OnRemoveMaterialFromInventory -= RemoveItem; // Unsubscribe from item drop event
    }

    public void AddItem(GameItemSO item, int amount)
    {
        if (inventory.ContainsKey(item.id))
        {
            inventory[item.id].quantity += amount;
        }
        else
        {
            inventory.Add(item.id, new InventoryItem(item, amount));
            //inventory[item.id] = new InventoryItem(item, amount);
        }
        Debug.Log($"Added {amount} {item.itemName} to inventory");

        OnInventoryChanged?.Invoke(inventory[item.id]);
    }

    public void RemoveItem(GameItemSO item, int amount)
    {
        InventoryItem removedInventoryItem = inventory[item.id];
        if (inventory.ContainsKey(item.id))
        {
            inventory[item.id].quantity -= amount;
            removedInventoryItem = inventory[item.id];
            if (inventory[item.id].quantity <= 0)
            {
                Debug.Log($"Item {item.itemName} has {inventory[item.id].quantity} int inventory");
                //removedInventoryItem = inventory[item.id];
                inventory.Remove(item.id);
            }
        }
        else
        {
            Debug.Log($"Item {item.itemName} not found in inventory");
            return;
        }
        Debug.Log($"removed {amount} {item.itemName} from inventory");
        OnInventoryChanged?.Invoke(removedInventoryItem);
    }

    public Dictionary<int, InventoryItem> GetInventory()
    {
        return inventory;
    }   

    public void ClearInventory()
    {
        inventory.Clear();
        OnInventoryClear?.Invoke(); 
    }

    public void ReloadInventory(List<InventoryItem> items)
    {
          
        foreach (InventoryItem item in items)
        {
            AddItem(item.gameItem, item.quantity);
        }
        Debug.Log($"Inventory reloaded, size: ${inventory.Count}");
    }

    public bool HasItem(int itemId, int amount)
    {
        return inventory.ContainsKey(itemId) && inventory[itemId].quantity >= amount;
    }
}

