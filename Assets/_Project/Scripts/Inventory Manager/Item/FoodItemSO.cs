using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "FoodItem", menuName = "Scriptable Objects/Items/Food")]
    public class FoodItemSO : GameItemSO
    {
        //food atributes
        public int hungerReplenishment;
        public int staminaReplenishment;

        public override void UseItem ()
        {
            Debug.Log($"Eating {itemName}, Restores: {hungerReplenishment} Hunger");
        }
    }
}
