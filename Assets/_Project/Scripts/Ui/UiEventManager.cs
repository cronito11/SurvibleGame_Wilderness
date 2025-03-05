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

        private static Action _currentCloseEvent;

        private void OnEnable()
        {
            OpenMainMenu();
        }

        public static void OpenMainMenu()
        {
            _currentCloseEvent?.Invoke();
            OnMainMenuOpened?.Invoke();
            _currentCloseEvent = OnMainMenuClosed;
        }

        public static void OpenOptionMenu()
        {
            _currentCloseEvent?.Invoke();
            OnOptionMenuOpened?.Invoke();
            _currentCloseEvent = OnOptionMenuClosed;
        }

        public static void CloseOptionMenu()
        {
            OnOptionMenuClosed?.Invoke();
        }

        public static void OpenPauseMenu()
        {
            _currentCloseEvent?.Invoke();
            OnPauseMenuOpened?.Invoke();
            _currentCloseEvent = OnPauseMenuClosed;
        }

        public static void OpenInventory()
        {
            _currentCloseEvent?.Invoke();
            OnInventoryOpened?.Invoke();
            _currentCloseEvent = OnInventoryClosed;
        }

        public static void OpenGamePlayMenu()
        {
            _currentCloseEvent?.Invoke();
            OnGamePlayMenuOpened?.Invoke();
            _currentCloseEvent = OnGamePlayMenuClosed;
        }

        public static void OpenGameOverMenu()
        {
            _currentCloseEvent?.Invoke();
            OnGameOverMenuOpened?.Invoke();
            _currentCloseEvent = OnGameOverMenuClosed;
        }
    }
}
