using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class PauseMenuUimanager : MonoBehaviour, IUiPannel
    {
        // UI Components for the Main Menu
        [SerializeField] private GameObject pauseMenuPanel; // Pause Menu panel
        [SerializeField] private Button resumeButton; // Resume Button
        [SerializeField] private Button saveButton; // Save Button
        [SerializeField] private Button optionsButton; // Options Button
        [SerializeField] private Button exitButton; // Exit Button
        [SerializeField] private Button backButton; // Back Button

        private void OnEnable()
        {
            UiEventManager.OnPauseMenuOpened += Show;
            UiEventManager.OnPauseMenuClosed += Hide;

            // Attaching Button Listners
            resumeButton.onClick.AddListener(OnResumeButtonClicked);
            saveButton.onClick.AddListener(OnSaveButtonClicked);
            optionsButton.onClick.AddListener(OnOptionsButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
            backButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            UiEventManager.OnPauseMenuOpened -= Show;
            UiEventManager.OnPauseMenuClosed -= Hide;

            // Removing Button Listners
            resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            saveButton.onClick.RemoveListener(OnSaveButtonClicked);
            optionsButton.onClick.RemoveListener(OnOptionsButtonClicked);
            exitButton.onClick.RemoveListener(OnExitButtonClicked);
            backButton.onClick.RemoveAllListeners();
        }

        //  Mehtod for Resume button clicked
        private void OnResumeButtonClicked()
        {
            Debug.Log("Resume Button Clicked");
        }

        private void OnSaveButtonClicked()
        {
            Debug.Log("Save Button Clicked");
        }

        // Mehtod for Options button clicked
        private void OnOptionsButtonClicked()
        {
            UiEventManager.OpenOptionMenu();
            Debug.Log("Pause Menu Options Button Clicked");
        }

        // Mehtod for Exit button clicked
        private void OnExitButtonClicked()
        {
            Debug.Log("Pause Menu Exit Button Clicked");
        }

        // Show Panel
        public void Show()
        {
            pauseMenuPanel.SetActive(true);
        }
        // Hide Panel 
        public void Hide()
        {
            pauseMenuPanel.SetActive(false);
        }
        // Check if panel is active
        public bool IsActive()
        {
            return pauseMenuPanel.activeSelf;
        }
    }
}
