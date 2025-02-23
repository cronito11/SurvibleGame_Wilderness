using UnityEngine;

[ System.Serializable ]
public class InventoryItem
{
    [field: SerializeField] 
    public GameItem gameItem 
    { 
        get;  set; 
    }
       
    [field: SerializeField]
    public int quantity
    {
        get; set;
    }

    [field: SerializeField]
    public int currentDurability // Only for weapons/armor
    {
        get;  set;
    }
    [field: SerializeField]
    public bool isAssignedToSlot // Only for weapons/armor
    {
        get; set;
    }

    public InventoryItem(GameItem item, int quantity)
    {
        this.gameItem = item;
        this.quantity = quantity;
    }

    public virtual void UseItem()
    {
        Debug.Log($"Using {gameItem.itemName}");
    }   
}
