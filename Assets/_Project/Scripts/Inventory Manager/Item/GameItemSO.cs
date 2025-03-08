using UnityEngine;
using UnityEngine.UIElements;
public enum ItemType { Weapon, Food, Material, Clothing }
public enum MaterialType { Wood, Stond, Iron }

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

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Scriptable Objects/Items/Weapon")]
public class WeaponItemSO : GameItemSO
{
    //weapon attributes
    public int damage;
    public int maxDurability;

    public override void UseItem()
    {
        Debug.Log($"Attacking with {itemName}, Damage: {damage}");
    }
}

[CreateAssetMenu(fileName = "FoodItem", menuName = "Scriptable Objects/Items/Food")]
public class FoodItemSO : GameItemSO
{
    //food atributes
    public int hungerReplenishment;
    public int staminaReplenishment;

    public override void UseItem()
    {
        Debug.Log($"Eating {itemName}, Restores: {hungerReplenishment} Hunger");
    }
}

[CreateAssetMenu(fileName = "Cloth Item", menuName = "Scriptable Objects/Items/Cloth")]
public class ClothItemSO : GameItemSO
{
    //cllothing attributes

    public override void UseItem()
    {
        
    }
}

[CreateAssetMenu(fileName = "Material Item", menuName = "Scriptable Objects/Items/Material")]
public class MaterialItemSO : GameItemSO
{
    MaterialType materialType;
    //material attributes
    public override void UseItem()
    {

    }
}