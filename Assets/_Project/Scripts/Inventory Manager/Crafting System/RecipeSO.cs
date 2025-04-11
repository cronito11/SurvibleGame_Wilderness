using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "RecipeSO", menuName = "Scriptable Objects/Recipe")]
    public class RecipeSO : ScriptableObject
    {
        public GameItemSO output;
        public int outputQuantity;
        public List<Ingredient> ingredients;
    }
}
