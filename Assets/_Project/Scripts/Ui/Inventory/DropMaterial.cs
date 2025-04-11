using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class DropMaterial : MonoBehaviour
    {
        public static event Action<InventoryItem> OnDropMaterialClicked;
        public static event Action<GameItemSO, int> OnRemoveMaterialFromInventory;// Event to notify when the drop button is clicked

        [SerializeField] private Button dropButton;
        [SerializeField] private Button backButton; // Back button to close the drop panel
        [SerializeField] private TMP_InputField amountInputField;
        

        [SerializeField] GameObject dropMaterialPanel; // The panel to drop the material

        private InventoryItem material; // The material to be dropped
        private GameItemSO gameItem; // The game item associated with the material
        private int availableQuantityOfSelectedItem; // The avaiable quantity of the selected item the one we want to drop    

        private void OnEnable()
        {
            InventoryItem.OnDropOnCraftingPanel += OnDropBegin; // Subscribe to the event
            dropButton.onClick.AddListener(DropItemToCraftingPanel); // Add listener to the drop button 
            backButton.onClick.AddListener(() => OpenDropPanel(false)); // Add listener to the back button  
        }

        private void OnDisable()
        {
            InventoryItem.OnDropOnCraftingPanel -= OnDropBegin; // Unsubscribe from the event
            dropButton.onClick.RemoveListener(DropItemToCraftingPanel); // Remove listener from the drop button
            backButton.onClick.RemoveListener(() => OpenDropPanel(false)); // Remove listener from the back button
        }

        private void DropItemToCraftingPanel()
        {
            int amount = int.Parse(amountInputField.text); // Parse the amount from the input field 
            if (amount <= 0)
            {
                Debug.Log("Invalid amount!"); // Log if the amount is invalid
                return;
            }
            if (amount > availableQuantityOfSelectedItem)
            {
                Debug.Log("Not enough quantity!"); // Log if the amount is greater than the available quantity
                return;
            }

            material = new InventoryItem(gameItem, amount); // Create a new inventory item with the specified amount

            OnDropMaterialClicked?.Invoke(material); // Invoke the event with the material and amount this will add the item to the crafting system script
            OnRemoveMaterialFromInventory?.Invoke(material.gameItem, amount); // Notify the inventory to remove the material    
            OpenDropPanel(false); // Hide the drop material panel
        } 
        void OnDropBegin(InventoryItem _material)
        {
            gameItem = _material.gameItem; // Assign the game item associated with the material
            availableQuantityOfSelectedItem = _material.quantity; // Get the quantity of the selected item
            //open pannel
            OpenDropPanel(true); // Show the drop material panel

            // Logic to drop the material
            Debug.Log("Material dropped!");
        }
        
        private void OpenDropPanel(bool _status)
        {
            dropMaterialPanel.SetActive(_status); // Show the drop material panel
        }
    }
}
