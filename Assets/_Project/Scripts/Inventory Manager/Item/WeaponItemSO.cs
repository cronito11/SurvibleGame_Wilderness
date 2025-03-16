using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Scriptable Objects/Items/Weapon")]
    public class WeaponItemSO : GameItemSO
    {
        //weapon attributes
        public int damage;
        public int maxDurability;

        public override void UseItem ()
        {
            Debug.Log($"Attacking with {itemName}, Damage: {damage}");
        }
    }
}
