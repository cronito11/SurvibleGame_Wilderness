using UnityEngine;

namespace Surviblewilderness
{
    public class OptionMenuUiManager : MonoBehaviour, IUiPannel
    {
        [SerializeField] private GameObject optionMenuPannel;

        private void OnEnable()
        {
            UiEventManager.OnOptionMenuOpened += Show;
            UiEventManager.OnOptionMenuClosed += Hide;
        }


        private void OnDisable()
        {
            UiEventManager.OnOptionMenuOpened -= Show;
            UiEventManager.OnOptionMenuClosed -= Hide;
        }

        public void Hide()
        {
            optionMenuPannel.SetActive(false);
        }

        public bool IsActive()
        {
            return optionMenuPannel.activeSelf;
        }

        public void Show()
        {
            optionMenuPannel.SetActive(true);
        }
    }
}
