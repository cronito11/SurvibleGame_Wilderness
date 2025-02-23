using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory :  MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private Dictionary<int, InventoryItem> inventory = new Dictionary<int, InventoryItem>();

    public static event Action OnInventoryChanged;  // 🔥 Event for inventory updates

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //inventorySlotsClothing = 
    }

    private void OnEnable()
    {
        InteractableObject.OnItemPickedUp += AddItem;
    }

    private void OnDisable()
    {
        InteractableObject.OnItemPickedUp -= AddItem;
    }

    public void AddItem(GameItem item, int amount)
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

        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(GameItem item, int amount)
    {
        if (inventory.ContainsKey(item.id))
        {
            inventory[item.id].quantity -= amount;
            if (inventory[item.id].quantity <= 0)
            {
                inventory.Remove(item.id);
            }
        }
        OnInventoryChanged?.Invoke();
    }

    public Dictionary<int, InventoryItem> GetInventory()
    {
        return inventory;
    }   

    public bool HasItem(int itemId, int amount)
    {
        return inventory.ContainsKey(itemId) && inventory[itemId].quantity >= amount;
    }
}

