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

public enum GameElement
{
    None,   
    Material_Wood,
    Material_Stone,
    Material_Iron,
    Material_Stick,
    Material_Diamond,
    Material_RareDiamond,
    Weapon_IronSword,
    Weapon_IronAxe,
    Weapon_SilverSword,
    Weapon_SilverAxe,
    Food_Apple,
    Food_Meat,
    Cloth_HavelsArmor,
}

[CreateAssetMenu(fileName = "GameItem", menuName = "Scriptable Objects/GameItem")]
public class GameItemSO : ScriptableObject
{
    //general attributes of a game item
    public int id;
    public string itemName;
    public ItemType itemType;
    public GameElement gameElement;
    public float weight;
    public int maxStackSize;
    public Sprite icon;
    
    public virtual void UseItem()
    {
        Debug.Log($"Using {itemName}");
    }
}





