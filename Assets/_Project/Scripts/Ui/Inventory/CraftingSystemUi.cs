using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class CraftingSystemUi : MonoBehaviour
    {
        public static event Action OnCraftButtonClicked; // Event to notify when crafting is completed

        [SerializeField] private Button craftButton;
        [SerializeField] List<CraftingSlot> craftingItems = new List<CraftingSlot>();
        [SerializeField] OutputSlot outputSlot;
        private void Awake()
        {
            craftingItems = GetComponentsInChildren<CraftingSlot>().ToList();
        }

        private void OnEnable()
        {
            DropMaterial.OnDropMaterialClicked += UpdateCraftingSlots;
            craftButton.onClick.AddListener(CraftButtonClicked);
            CraftingSystem.OnSuccessfulCraft += SetOutputSlot; // Subscribe to the event
            CraftingSystem.OnSuccessfulCraft += ClearAllCraftingSlots; // Subscribe to the event
        }
        private void OnDisable()
        {
            DropMaterial.OnDropMaterialClicked -= UpdateCraftingSlots;
            craftButton.onClick.RemoveListener(CraftButtonClicked);
            CraftingSystem.OnSuccessfulCraft -= SetOutputSlot; // Unsubscribe from the event
            CraftingSystem.OnSuccessfulCraft -= ClearAllCraftingSlots; // Unsubscribe from the event
        }

        private void SetOutputSlot(InventoryItem _outputItem)
        {
            outputSlot.SetItem(_outputItem);
        }

        private void UpdateCraftingSlots(InventoryItem ingredient)
        {
            //find an empty slot and assign the _outputItem to it
            foreach (var item in craftingItems)
            {
                if (item.isEmpty)
                {
                    item.SetItem(ingredient);

                    break;
                }
            }
        }

        private void ClearAllCraftingSlots(InventoryItem _outputItem)
        {
            foreach (var item in craftingItems)
            {
                item.EmptySlot();
            }
        }

        private void CraftButtonClicked()
        {
            OnCraftButtonClicked?.Invoke(); // Invoke the event to notify that Crafting is initiated    
        }
    }
}
