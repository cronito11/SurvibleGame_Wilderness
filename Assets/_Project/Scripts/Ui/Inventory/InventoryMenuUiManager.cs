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
        [SerializeField] private Button backButton;

        private void OnEnable()
        {
            UiEventManager.OnInventoryOpened += Show;
            UiEventManager.OnInventoryClosed += Hide;
            UiEventManager.OnInventoryCategorySelected += InventoryCategoryManager.ShowInventoryCategory;

            // Attaching Button Listners
            foodButton.onClick.AddListener(() => UiEventManager.SelectInventoryCategory(InventoryType.Food));
            clothingButton.onClick.AddListener(() => UiEventManager.SelectInventoryCategory(InventoryType.Clothing));
            weaponsButton.onClick.AddListener(() => UiEventManager.SelectInventoryCategory(InventoryType.Weapons));
            materialsButton.onClick.AddListener(() => UiEventManager.SelectInventoryCategory(InventoryType.Materials));
            backButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            UiEventManager.OnInventoryOpened -= Show;
            UiEventManager.OnInventoryClosed -= Hide;

            // Removing Button Listners
            foodButton.onClick.RemoveAllListeners();
            clothingButton.onClick.RemoveAllListeners();
            weaponsButton.onClick.RemoveAllListeners();
            materialsButton.onClick.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
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
