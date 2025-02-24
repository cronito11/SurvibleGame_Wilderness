using System;
using UnityEngine;

namespace Surviblewilderness
{
    
    public class UiManager : MonoBehaviour
    {
        public static event Action OnButtonClick;

        [SerializeField] private GameObject inventoryUi;

        [SerializeField] private GameObject foodItemsInventory;
        [SerializeField] private GameObject cothingItemsInventory;
        [SerializeField] private GameObject weaponItemsInventory;
        [SerializeField] private GameObject materialItemsInventory;
        [SerializeField] private GameObject characterStates;
        [SerializeField] private GameObject craftingPannel;

        private bool isInventoryOpen = false;

        #region MainMenu Ui 
        public void OnStartGameClick()
        {
            Debug.Log("Start Game Clicked");  
            OnButtonClick?.Invoke();    
        }

        public void OnLoadGameClick()
        {
            Debug.Log("Load Game Clicked");
            OnButtonClick?.Invoke();
        }

        public void OnOptionsClick()
        {
            Debug.Log("Settings Clicked");
            OnButtonClick?.Invoke();
        }

        public void OnMainMenuQuitClick()
        {
            Debug.Log("Quit Clicked");
            OnButtonClick?.Invoke();
        }
        #endregion

        #region PauseMenu Ui    

        public void OnResumeClick()
        {
            Debug.Log("Resume Clicked");
            OnButtonClick?.Invoke();
        }

        public void OnSaveClick()
        {
            Debug.Log("Save Clicked");
            OnButtonClick?.Invoke();
        }

        public void OnQuitToMainMenu()
        {
            Debug.Log("Quiting to Main Menu clicked");
            OnButtonClick?.Invoke();
        }

        

        #endregion

        #region Inventory Ui
        public void OnInventoryOpen()
        {
            Debug.Log("Inventory Opened");
            OnButtonClick?.Invoke();
        }

        public void OnInventoryClick()
        {
            //Debug.Log("Inventory clicked");

            inventoryUi.SetActive(!isInventoryOpen);

            isInventoryOpen = !isInventoryOpen;
            OnButtonClick?.Invoke();

        }

        public void OnClothinhOptionClicked()
        {
            Debug.Log("Clothing option clicked");
            cothingItemsInventory.SetActive(true);
            foodItemsInventory.SetActive(false);
            weaponItemsInventory.SetActive(false);
            materialItemsInventory.SetActive(false);
            characterStates.SetActive(true);
            craftingPannel.SetActive(false);
            OnButtonClick?.Invoke();

        }

        public void OnWeaponOptionClicked()
        {
            Debug.Log("Weapon option clicked");
            cothingItemsInventory.SetActive(false);
            foodItemsInventory.SetActive(false);
            weaponItemsInventory.SetActive(true);
            materialItemsInventory.SetActive(false);
            characterStates.SetActive(true);
            craftingPannel.SetActive(false);
            OnButtonClick?.Invoke();
        }

        public void OnMaterialOptionClicked()
        {
            Debug.Log("Material option clicked");
            cothingItemsInventory.SetActive(false);
            foodItemsInventory.SetActive(false);
            weaponItemsInventory.SetActive(false);
            materialItemsInventory.SetActive(true);
            characterStates.SetActive(false);
            craftingPannel.SetActive(true);
            OnButtonClick?.Invoke();

        }

        public void OnFoodOptionClicked()
        {
            Debug.Log("Food option clicked");
            cothingItemsInventory.SetActive(false);
            foodItemsInventory.SetActive(true);
            weaponItemsInventory.SetActive(false);
            materialItemsInventory.SetActive(false);
            characterStates.SetActive(true);
            craftingPannel.SetActive(false);
            OnButtonClick?.Invoke();

        }
        #endregion

        #region Crafting Ui

        #endregion
    }
}
