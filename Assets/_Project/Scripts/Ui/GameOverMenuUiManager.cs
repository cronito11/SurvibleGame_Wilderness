using UnityEngine;

namespace Surviblewilderness
{
    public class GameOverMenuUiManager : MonoBehaviour, IUiPannel
    {
        [SerializeField] private GameObject gameOverMenuPannel;

        private void OnEnable()
        {
            UiEventManager.OnGameOverMenuOpened += Show;
            UiEventManager.OnGameOverMenuClosed += Hide;
        }

        private void OnDisable()
        {
            UiEventManager.OnGameOverMenuOpened -= Show;
            UiEventManager.OnGameOverMenuClosed -= Hide;
        }

        public void Hide()
        {
            gameOverMenuPannel.SetActive(false);
        }

        public bool IsActive()
        {
            return gameOverMenuPannel.activeSelf;
        }

        public void Show()
        {
            gameOverMenuPannel.SetActive(true);
        }
    }
}
