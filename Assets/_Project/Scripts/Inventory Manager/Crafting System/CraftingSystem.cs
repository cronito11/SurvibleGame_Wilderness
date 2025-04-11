using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Surviblewilderness
{
    public class CraftingSystem : MonoBehaviour
    {
        public static event Action<InventoryItem> OnSuccessfulCraft; // Event to notify when crafting is completed

        [SerializeField] private Transform craftingSlotsGrid;
        [SerializeField] private Transform outputSlotGrid;
        
        [SerializeField] private List<RecipeSO> allRecipes;
        [SerializeField] private List<InventoryItem> inputIngredients = new List<InventoryItem>();

        private void OnEnable()
        {
            CraftingSystemUi.OnCraftButtonClicked += TryCraft; // Subscribe to the event
            DropMaterial.OnDropMaterialClicked += AddItem; // Subscribe to the event    
            CraftingSlot.OnRemoveMaterialFromCraftingPanel += RemoveItem; // Subscribe to the event
        }
        private void OnDisable()
        {
            CraftingSystemUi.OnCraftButtonClicked -= TryCraft; // Unsubscribe from the event
            DropMaterial.OnDropMaterialClicked -= AddItem; // Unsubscribe from the event
            CraftingSlot.OnRemoveMaterialFromCraftingPanel -= RemoveItem; // Unsubscribe from the event
        }


        public void AddItem(InventoryItem item)
        {
            foreach(InventoryItem inputItem in inputIngredients)
            {
                if(inputItem.gameItem.id == item.gameItem.id)
                {
                    inputItem.quantity += item.quantity;
                    return;
                }   
            }
            inputIngredients.Add(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            if (inputIngredients.Contains(item))
            {
                inputIngredients.Remove(item);
            }
            else
            {
                Debug.Log("Item not found in input ingredients.");
            }
        }

        private void ClearInputIngredients()
        {
            inputIngredients.Clear();
        }   

        public void TryCraft()
        {
            bool matchesRecipe = true;
            foreach (RecipeSO recipe in allRecipes)
            {
                List<Ingredient> recipeIngredients = recipe.ingredients;

                // Quick fail: not the same number of ingredients
                if (inputIngredients.Count != recipeIngredients.Count)
                {
                    Debug.Log($"Not the same number of intputingredients{inputIngredients.Count}.");   
                    continue;
                }
                
                // Match ingredients by removing found ones
                foreach (Ingredient ingredient in recipeIngredients)
                {
                    int inputCount = inputIngredients.Count;
                    int lookingForIngredientQuantity = ingredient.quantity;

                    for (int i = 0; i < inputIngredients.Count; i++) 
                    {
                        Debug.Log($"Looking for {ingredient.materialType} x{lookingForIngredientQuantity} in input list");

                        if (lookingForIngredientQuantity <= 0)
                        {
                            break;
                        }
                        // Check if the requied quantity of ingredient is in the input list or not

                        if (inputIngredients[i].gameItem is not MaterialItemSO materialItem)
                        {
                            continue;
                        }
                        if (materialItem.materialType != ingredient.materialType)
                        {
                            continue;
                        }

                        if (inputIngredients[i].quantity >= lookingForIngredientQuantity)
                        {
                            inputIngredients[i].quantity -= lookingForIngredientQuantity;
                            lookingForIngredientQuantity = 0;
                            break;
                        }
                        else
                        {
                            lookingForIngredientQuantity -= inputIngredients[i].quantity;
                            inputIngredients[i].quantity = 0;
                        }
                    }

                    //// Check if the requied quantity of ingredient is in the input list or not 
                    //int index = 0;
                    //while (index < inputCount && lookingForIngredientQuantity > 0)
                    //{

                    //    //look for the ingredient in the input list
                    //    if (inputIngredients[index].gameItem is not MaterialItemSO materialItem)
                    //    {
                    //        index++;
                    //        continue;
                    //    }

                    //    if (materialItem.materialType != ingredient.materialType)
                    //    {
                    //        index++;
                    //        continue;
                    //    }
                        
                    //    lookingForIngredientQuantity -= inputIngredients[index].quantity;
                    //    index++;
                    //}

                    if(lookingForIngredientQuantity > 0)
                    {
                        matchesRecipe = false;
                        Debug.Log($"Not enough {ingredient.materialType} in input");
                        break;
                    }

                    //// Find matching ingredient in input
                    //InventoryItem inputMatch = inputIngredients.Find(input =>
                    //    (input.gameItem as MaterialItemSO).materialType == ingredient.materialType 
                    //    && input.quantity >= ingredient.quantity);

                    //if (inputMatch == null)
                    //{
                    //    matchesRecipe = false;
                    //    Debug.Log($"{ingredient.materialType} not found in input");
                    //    break;
                    //}
                }

                if (matchesRecipe)
                {
                    Debug.Log("Crafting: " + recipe.output.name);
                    InventoryItem outputItem = new InventoryItem(recipe.output, recipe.outputQuantity);
                    Debug.Log($"Crafted {outputItem.gameItem.itemName} x{outputItem.quantity}");
                    OnSuccessfulCraft?.Invoke(outputItem);
                    ClearInputIngredients();    
                }
            }

            // No matching recipe found
            Debug.Log("No valid recipe found.");
            //return null;
        }
    }
}
