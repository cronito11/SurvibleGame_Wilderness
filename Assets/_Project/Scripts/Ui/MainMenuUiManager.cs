using UnityEngine;

namespace Surviblewilderness
{
    public class MainMenuUiManager : MonoBehaviour, IUiPannel
    {
        [SerializeField] private GameObject mainMenuPannel;

        private void OnEnable()
        {
            UiEventManager.OnMainMenuOpened += Show;
            UiEventManager.OnMainMenuClosed += Hide;
        }

        private void OnDisable()
        {
            UiEventManager.OnMainMenuOpened -= Show;
            UiEventManager.OnMainMenuClosed -= Hide;
        }

        public void Hide()
        {
            mainMenuPannel.SetActive(false);
        }

        public bool IsActive()
        {
            return mainMenuPannel.activeSelf;
        }

        public void Show()
        {
            mainMenuPannel.SetActive(true);
        }
    }
}
