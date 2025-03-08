using UnityEngine;
using System;

namespace Surviblewilderness
{
    public class UiEventManager : MonoBehaviour
    {
        public static event Action OnMainMenuOpened;
        public static event Action OnOptionMenuOpened;
        public static event Action OnPauseMenuOpened;
        public static event Action OnGamePlayMenuOpened;
        public static event Action OnInventoryOpened;
        public static event Action OnGameOverMenuOpened;

        public static event Action OnMainMenuClosed;
        public static event Action OnOptionMenuClosed;
        public static event Action OnPauseMenuClosed;
        public static event Action OnGamePlayMenuClosed;
        public static event Action OnInventoryClosed;
        public static event Action OnGameOverMenuClosed;

        // Event for inventory category selection
        public static event Action<InventoryType> OnInventoryCategorySelected;

        private static Action _currentCloseEvent;

        public static Action OnButtonClick;

        private void OnEnable()
        {
            //OpenMainMenu();
        }

        public static void OpenMainMenu()
        {
            _currentCloseEvent?.Invoke();
            OnMainMenuOpened?.Invoke();
            _currentCloseEvent = OnMainMenuClosed;
            OnButtonClick?.Invoke();
        }

        public static void OpenOptionMenu()
        {
            _currentCloseEvent?.Invoke();
            OnOptionMenuOpened?.Invoke();
            _currentCloseEvent = OnOptionMenuClosed;
            OnButtonClick?.Invoke();
        }

        public static void OpenPauseMenu()
        {
            _currentCloseEvent?.Invoke();
            OnPauseMenuOpened?.Invoke();
            _currentCloseEvent = OnPauseMenuClosed;
            OnButtonClick?.Invoke();
        }

        public static void OpenInventory()
        {
            _currentCloseEvent?.Invoke();
            OnInventoryOpened?.Invoke();
            _currentCloseEvent = OnInventoryClosed;
            OnButtonClick?.Invoke();
        }

        public static void OpenGamePlayMenu()
        {
            _currentCloseEvent?.Invoke();
            OnGamePlayMenuOpened?.Invoke();
            _currentCloseEvent = OnGamePlayMenuClosed;
            OnButtonClick?.Invoke();
        }

        public static void OpenGameOverMenu()
        {
            _currentCloseEvent?.Invoke();
            OnGameOverMenuOpened?.Invoke();
            _currentCloseEvent = OnGameOverMenuClosed;
            OnButtonClick?.Invoke();
        }

        // Method to select inventory category
        public static void SelectInventoryCategory(InventoryType categoty)
        {
            OnInventoryCategorySelected?.Invoke(categoty);
            OnButtonClick?.Invoke();
        }

    }
}
