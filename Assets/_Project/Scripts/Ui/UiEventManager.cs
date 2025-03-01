using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
namespace Surviblewilderness
{
    public class UiEventManager : MonoBehaviour
    {
        [Header("Option Panel Buttons")]
        [SerializeField] private Button optionButton;

        [Header("Inventory Panel Buttons")]
        [SerializeField] private Button inventoryButton;

        [Header("Pause Panel Buttons")]
        [SerializeField] private Button pauseButton;

        [Header("Game Over Pannel Buttons")]
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Button quitToMainMenu;
         
        

        public static event Action OnButtonClick;
        public static event Action OnMainMenuOpened;
        public static event Action OnInventoryOpened;
        public static event Action OnPauseMenuOpened;
        public static event Action OnOptionMenuOpened;
        public static event Action OnGameOverMenuOpened;

        public static event Action OnMainMenuClosed;
        public static event Action OnInventoryClosed;
        public static event Action OnPauseMenuClosed;
        public static event Action OnOptionMenuClosed;
        public static event Action OnGameOverMenuClosed;

        private static Action _currentCloseEvent;

        private void OnEnable()
        {
            optionButton.onClick.AddListener(OpenOptionMenu);
            inventoryButton.onClick.AddListener(OpenInventory);
            pauseButton.onClick.AddListener(OpenPauseMenu);

        }

        private void OnDisable()
        {
            optionButton.onClick.RemoveListener(OpenOptionMenu);
            inventoryButton.onClick.RemoveListener(OpenInventory);
            pauseButton.onClick.RemoveListener(OpenPauseMenu);
        }

        private void Start()
        {
            OpenMainMenu();
        }
                
        public void OpenMainMenu()
        {
            _currentCloseEvent?.Invoke();
            OnMainMenuOpened?.Invoke();
            _currentCloseEvent = OnMainMenuClosed;
        }

        public void OpenInventory()
        {
            _currentCloseEvent?.Invoke();
            OnInventoryOpened?.Invoke();
            _currentCloseEvent = OnInventoryClosed;
        }

        public void OpenPauseMenu()
        {
            _currentCloseEvent?.Invoke();
            OnPauseMenuOpened?.Invoke();
            _currentCloseEvent = OnPauseMenuClosed;
        }

        public void OpenOptionMenu()
        {
            _currentCloseEvent?.Invoke();
            OnOptionMenuOpened?.Invoke();
            _currentCloseEvent = OnOptionMenuClosed;
        }

        public void OpenGameOverMenu()
        {
            _currentCloseEvent?.Invoke();
            OnGameOverMenuOpened?.Invoke();
            _currentCloseEvent = OnGameOverMenuClosed;
        }
    }
}
