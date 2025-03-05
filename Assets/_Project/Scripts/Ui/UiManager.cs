using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Surviblewilderness
{

    public class UiManager : MonoBehaviour
    {
        // Events triggered when a button is clicked or the game starts
        public static event Action OnButtonClick; // Invoked when any UI button is clicked
        public static event Action OnGameStart; // Invoked when the game starts

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

        // Track inventory open/close state
        private bool isInventoryOpen = false;

        private void Start()
        {
            // Play main menu theme music when the game starts
            AudioManager.Instance.PlayMainMenuTheme();
            // Pause game time until the player starts the game
            Time.timeScale = 0;
        }

        // Closes the specified UI Panel
        public void OnClosePannel(GameObject pannel)
        {
            pannel.SetActive(false);
            OnButtonClick?.Invoke(); // Notify listners that a button was clicked
        }

        // Main Menu UI
        // Start the game, enabling in-game UI elements and resuming time.
        #region MainMenu Ui 
        public void OnStartGameClick()
        {
            Time.timeScale = 1; // Resume game time
            Debug.Log("Start Game Clicked");
            OnButtonClick?.Invoke();

            mainMenuPannel.SetActive(false); // Hide  the main menu
            inGameUiElements.SetActive(true); // Show in-game UI
            OnGameStart?.Invoke(); // Trigger game start event
            //CloseAllAndOpenThisPannel(inGameUiElements);    
        }

        // Loads a previously saved game 
        public void OnLoadGameClick()
        {
            Debug.Log("Load Game Clicked");
            OnButtonClick?.Invoke();
        }

        // Opens the options menu
        public void OnOptionsClick()
        {
            Debug.Log("Settings Clicked");
            OnButtonClick?.Invoke();
            CloseAllAndOpenThisPannel(optionMenuPannel);
        }

        // Quits the game
        public void OnQuitGame()
        {
            Debug.Log("Quit Clicked");
            OnButtonClick?.Invoke();
            Time.timeScale = 0;
            Application.Quit();
        }
        #endregion

        // Pause Game UI
        #region PauseMenu Ui    
        public void OnPauseClick()
        {
            Debug.Log("Pause Clicked");
            OnButtonClick?.Invoke();
            CloseAllAndOpenThisPannel(pauseMenuPannel);
        }

        // Resumes the game from the pause menu.
        public void OnResumeClick()
        {
            Debug.Log("Resume Clicked");
            OnButtonClick?.Invoke();
            CloseAllAndOpenThisPannel(inGameUiElements);
        }

        // Saves the current game state
        public void OnSaveClick()
        {
            Debug.Log("Save Clicked");
            OnButtonClick?.Invoke();
        }

        // Quits to the main menu from the pause menu.
        public void OnQuitToMainMenu()
        {
            Debug.Log("Quiting to Main Menu clicked");
            OnButtonClick?.Invoke();
        }
        #endregion

        // Inventory UI
        #region Inventory Ui
        public void OnInventoryOpen()
        {
            Debug.Log("Inventory Opened");
            OnButtonClick?.Invoke();
        }
        /// Toggles the inventory panel on or off.
        public void OnInventoryClick()
        {
            //Debug.Log("Inventory clicked");
            inventoryPannel.SetActive(!isInventoryOpen);
            isInventoryOpen = !isInventoryOpen;
            OnButtonClick?.Invoke();

        }

        // Displays clothing items in the inventory.
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

        // Displays weapon items in the inventory.
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

        // Displays material items in the inventory.
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

        // Displays food items in the inventory.
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

        // Crafting UI
        #region Crafting Ui
        #endregion


        // Closes all panels and opens only the specified one.
        #region Uitility
        private void CloseAllAndOpenThisPannel(GameObject _pannel)
        {
            foreach (var pannel in pannels)
            {
                if (pannel == _pannel)
                    pannel.SetActive(true);
                else
                    pannel.SetActive(false);
            }
        }
        #endregion
    }
}
