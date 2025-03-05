using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class InventoryMenuUiManager : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Button foodButton;
        [SerializeField] private Button clothingButton;
        [SerializeField] private Button weaponsButton;
        [SerializeField] private Button materialsButton;

        private void OnEnable()
        {
            UiEventManager.OnInventoryOpened += Show;
            UiEventManager.OnInventoryClosed += Hide;

            // Attaching Button Listners
            foodButton.onClick.AddListener(OnFoodButtonClicked);
            clothingButton.onClick.AddListener(OnClothingButtonClicked);
            weaponsButton.onClick.AddListener(OnWeaponsButtonClicked);
            materialsButton.onClick.AddListener(OnMaterialsButtonClicked);
        }

        private void OnDisable()
        {
            UiEventManager.OnInventoryOpened -= Show;
            UiEventManager.OnInventoryClosed -= Hide;

            // Removing Button Listners
            foodButton.onClick.RemoveListener(OnFoodButtonClicked);
            clothingButton.onClick.RemoveListener(OnClothingButtonClicked);
            weaponsButton.onClick.RemoveListener(OnWeaponsButtonClicked);
            materialsButton.onClick.RemoveListener(OnMaterialsButtonClicked);
        }

        //  Mehtod for Food button clicked
        private void OnFoodButtonClicked()
        {
            Debug.Log("Food Button Clicked");
        }

        //  Mehtod for Clothing button clicked
        private void OnClothingButtonClicked()
        {
            Debug.Log("Clothing Button Clicked");
        }

        //  Mehtod for Weapons button clicked
        private void OnWeaponsButtonClicked()
        {
            Debug.Log("Weapons Button Clicked");
        }

        //  Mehtod for Materials button clicked
        private void OnMaterialsButtonClicked()
        {
            Debug.Log("Materials Button Clicked");
        }


        // Show Panel
        public void Show()
        {
            inventoryPanel.SetActive(true);
        }
        // Hide Panel 
        public void Hide()
        {
            inventoryPanel.SetActive(false);
        }
        // Check if panel is active
        public bool IsActive()
        {
            return inventoryPanel.activeSelf;
        }

    }
}
