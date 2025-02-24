using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Surviblewilderness
{

    public class UiManager : MonoBehaviour
    {
        public static event Action OnButtonClick;
        public static event Action OnGameStart;
        [Header("Menu Pannels")]
        [SerializeField] private GameObject mainMenuPannel;
        [SerializeField] private GameObject pauseMenuPannel;
        [SerializeField] private GameObject optionMenuPannel;
        [SerializeField] private GameObject gameOverMenuPannel;
        [SerializeField] private List<GameObject> pannels;

        [Header("Inventory")]
        [SerializeField] private GameObject inGameUiElements;
        [SerializeField] private GameObject inventoryPannel;
        [SerializeField] private GameObject foodItemsInventory;
        [SerializeField] private GameObject cothingItemsInventory;
        [SerializeField] private GameObject weaponItemsInventory;
        [SerializeField] private GameObject materialItemsInventory;
        [SerializeField] private GameObject characterStates;
        [SerializeField] private GameObject craftingPannel;

        private bool isInventoryOpen = false;

        private void Start()
        {
            AudioManager.Instance.PlayMainMenuTheme();  
            Time.timeScale = 0;
        }
        public void OnClosePannel(GameObject pannel)
        {
            pannel.SetActive(false);
            OnButtonClick?.Invoke();
        }

        #region MainMenu Ui 
        public void OnStartGameClick()
        {
            Time.timeScale = 1;
            Debug.Log("Start Game Clicked");
            OnButtonClick?.Invoke();

            mainMenuPannel.SetActive(false);
            inGameUiElements.SetActive(true);
            OnGameStart?.Invoke();  
            //CloseAllAndOpenThisPannel(inGameUiElements);    
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
            CloseAllAndOpenThisPannel(optionMenuPannel);
        }

        public void OnQuitGame()
        {
            Debug.Log("Quit Clicked");
            OnButtonClick?.Invoke();
            Time.timeScale = 0;
            Application.Quit(); 
        }
        #endregion

        #region PauseMenu Ui    

        public void OnPauseClick()
        {
            Debug.Log("Pause Clicked");
            OnButtonClick?.Invoke();
            CloseAllAndOpenThisPannel(pauseMenuPannel);
        }

        public void OnResumeClick()
        {
            Debug.Log("Resume Clicked");
            OnButtonClick?.Invoke();
            CloseAllAndOpenThisPannel(inGameUiElements);
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

            inventoryPannel.SetActive(!isInventoryOpen);
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


        #region Uitility

        private void CloseAllAndOpenThisPannel(GameObject _pannel)
        {
            foreach (var pannel in pannels)
            {
                if (pannel == _pannel)
                    pannel.SetActive(true);
                else
                    pannel.SetActive(false);
                //pannel.SetActive(false);
            }
        }
        #endregion
    }
}
