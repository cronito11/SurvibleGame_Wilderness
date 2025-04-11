using UnityEngine;
using UnityEngine.UIElements;
public enum ItemType { Weapon, Food, Material, Clothing }
public enum MaterialType 
{
    None,
    Wood,
    Stone,
    Iron,
    Stick,
    Diamond,
    RareDiamond
}

[CreateAssetMenu(fileName = "GameItem", menuName = "Scriptable Objects/GameItem")]
public class GameItemSO : ScriptableObject
{
    //general attributes of a game item
    public int id;
    public string itemName;
    public ItemType itemType;
    public float weight;
    public int maxStackSize;
    public Sprite icon;
    
    public virtual void UseItem()
    {
        Debug.Log($"Using {itemName}");
    }
}





