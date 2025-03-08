using UnityEngine;

namespace Surviblewilderness
{
    public class InventoryCategoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject clothingItemsInventory;
        [SerializeField] private GameObject foodItemsInventory;
        [SerializeField] private GameObject weaponItemsInventory;
        [SerializeField] private GameObject materialItemsInventory;
        [SerializeField] private GameObject characterStates;
        [SerializeField] private GameObject craftingPanel;

        private static InventoryCategoryManager _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static void ShowInventoryCategory(InventoryType category)
        {
            if (_instance == null) return;

            _instance.clothingItemsInventory.SetActive(category == InventoryType.Clothing);
            _instance.foodItemsInventory.SetActive(category == InventoryType.Food);
            _instance.weaponItemsInventory.SetActive(category == InventoryType.Weapons);
            _instance.materialItemsInventory.SetActive(category == InventoryType.Materials);

            _instance.characterStates.SetActive(category != InventoryType.Materials);
            _instance.craftingPanel.SetActive(category == InventoryType.Materials);

            Debug.Log(category + " inventory selected.");

        }
    }
}
