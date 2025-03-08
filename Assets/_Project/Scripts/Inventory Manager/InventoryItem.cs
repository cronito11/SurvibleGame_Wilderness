using UnityEngine;

[ System.Serializable ]
public class InventoryItem
{
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
    public int currentDurability // Only for weapons/armor
    {
        get;  set;
    }
    [field: SerializeField]
    public bool isAssignedToSlot // Only for weapons/armor
    {
        get; set;
    }

    public InventoryItem(GameItemSO item, int quantity)
    {
        this.gameItem = item;
        this.quantity = quantity;
    }

    public virtual void UseItem()
    {
        Debug.Log($"Using {gameItem.itemName}");
    }   
}
