using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class MainMenuUiManager : MonoBehaviour, IUiPannel
    {
        // UI Components for the Main Menu
        [Header("Main Menu Panel Components")]
        [SerializeField] private GameObject mainMenuPannel; // Main Menu Panel
        [SerializeField] private Button startGameButton; // Start Game Button
        [SerializeField] private Button loadGameButton; // Load Game Button
        [SerializeField] private Button optionsMenuButton; // Option Menu Button
        [SerializeField] private Button exitGameButton; // Exit Game Button

        public static event Action OnButtonClick;


        private void OnEnable()
        {
            UiEventManager.OnMainMenuOpened += Show; // Menu Enabled when notified
            UiEventManager.OnMainMenuClosed += Hide; // Menu Disabled when notified

            startGameButton.onClick.AddListener(OnStartGameButtonClicked);
            loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
            optionsMenuButton.onClick.AddListener(OnOptionsMenuButtonClicked);
            exitGameButton.onClick.AddListener(OnExitGameButtonClicked);


        }

        private void OnDisable()
        {
            UiEventManager.OnMainMenuOpened -= Show;
            UiEventManager.OnMainMenuClosed -= Hide;

            // Removing Button Listners
            startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
            loadGameButton.onClick.RemoveListener(OnLoadGameButtonClicked);
            optionsMenuButton.onClick.RemoveListener(OnOptionsMenuButtonClicked);
            exitGameButton.onClick.RemoveListener(OnExitGameButtonClicked);
        }

        //  Mehtod for Start game button clicked
        private void OnStartGameButtonClicked()
        {
            Debug.Log("Start Game Button Clicked");
            OnButtonClick?.Invoke(); // Trigger the OnButtonClick event
            SceneManager.LoadScene("Level_Design");
            mainMenuPannel.SetActive(false);

        }

        // Mehtod for Load Game Button Clicked
        private void OnLoadGameButtonClicked()
        {
            OnButtonClick?.Invoke(); // Trigger the OnButtonClick event
            Debug.Log("Load Game Button Clicked");
        }

        // Mehtod for Options Menu Button Clicked
        private void OnOptionsMenuButtonClicked()
        {
            Debug.Log("Options Button Clicked");
            OnButtonClick?.Invoke(); // Trigger the OnButtonClick event
            UiEventManager.OpenOptionMenu();
        }

        // Mehtod for Exit Game Button Clickd
        private void OnExitGameButtonClicked()
        {
            OnButtonClick?.Invoke(); // Trigger the OnButtonClick event
            Debug.Log("Exit Button Clicked");
        }

        // Show Panel
        public void Show()
        {
            mainMenuPannel.SetActive(true);
        }

        // Hide Panel
        public void Hide()
        {
            mainMenuPannel.SetActive(false);
        }

        // Check if panel is active
        public bool IsActive()
        {
            return mainMenuPannel.activeSelf;
        }

    }
}
