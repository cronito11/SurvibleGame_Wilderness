using System;
using UnityEngine;

[ System.Serializable ]
public class InventoryItem 
{
    public static event Action<GameItemSO,int> OnItemUsed; // 🔥 Event for any item usage to remove it from inventory
    public static event Action<GameItemSO> OnConsumeFoodItem; // 🔥 Event for food item usage to generate health or other effects
    public static event Action<GameItemSO> OnConsumeWeaponItem; // 🔥 Event for weapon item usage to generate damage or other effects
    public static event Action<GameItemSO> OnConsumeClothingItem; // 🔥 Event for clothing item usage to generate armor or other effects


    private int defaultConsumeAmount = 1; // Default amount to consume when using an item

    [field: SerializeField] 
    public GameItemSO gameItem 
    { 
        get;  set; 
    }
       
    [field: SerializeField]
    public int quantity
    {
        get; set;
    }

    [field: SerializeField]
    public bool isStackable // Only for weapons/armor
    {
        get { return gameItem.maxStackSize > 1; }
    }
    [field: SerializeField]
    public bool isAssignedToSlot 
    {
        get; set;
    }

    public InventoryItem(GameItemSO item, int quantity)
    {
        this.gameItem = item;
        this.quantity = quantity;
    }

    public InventoryItem(GameItemSO item, int quantity, bool isAssignedToSlot)
    {
        this.gameItem = item;
        this.quantity = quantity;
        this.isAssignedToSlot = isAssignedToSlot;
    }

    public virtual void UseItem()
    {
        // Check if the item is consumable food, weapon, armor 

        switch (gameItem.itemType)
        {
            case ItemType.Food:
                // Use the food item
                ConsumeFoodItem();
                break;
            case ItemType.Weapon:
                // Use the weapon item
                EquipWeapon();
                break;
            case ItemType.Clothing:
                // Use the clothing item
                Debug.Log($"Wearing {gameItem.itemName}");
                break;
        }

        Debug.Log($"Using {gameItem.itemName}");
    
    }

    private void EquipWeapon()
    {
        OnConsumeWeaponItem?.Invoke(gameItem);  

        OnItemUsed?.Invoke(gameItem, defaultConsumeAmount);
        Debug.Log($"Equipping {gameItem.itemName}");
    }

    private void ConsumeFoodItem()
    {
        // Implement food item usage logic here
        //increase player health by x amount 
        OnConsumeFoodItem?.Invoke(gameItem);
        //remove the item from the inventory
        OnItemUsed?.Invoke(gameItem, defaultConsumeAmount);
        Debug.Log($"Eating {gameItem.itemName}");
    }
}
