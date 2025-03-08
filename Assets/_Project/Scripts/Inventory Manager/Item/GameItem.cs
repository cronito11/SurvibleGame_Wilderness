using UnityEngine;
using UnityEngine.UIElements;
public enum ItemType { Weapon, Food, Material, Clothing }

[CreateAssetMenu(fileName = "GameItem", menuName = "Scriptable Objects/GameItem")]
public class GameItem : ScriptableObject
{
    public int id;
    public string itemName;
    public ItemType itemType;
    public float weight;
    public int maxStackSize;
    public Sprite icon;
    //public int durability; // Only for weapons/armor
    //public bool isConsumable;

}
