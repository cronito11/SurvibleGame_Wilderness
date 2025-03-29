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
        [SerializeField] private Button loadButton; // Load Button
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
            loadButton.onClick.AddListener(OnLoadButtonClicked);
            optionsButton.onClick.AddListener(OnOptionsButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
            backButton.onClick.AddListener(OnBackButtonClicked);
        }

        private void OnDisable()
        {
            UiEventManager.OnPauseMenuOpened -= Show;
            UiEventManager.OnPauseMenuClosed -= Hide;

            // Removing Button Listners
            resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            saveButton.onClick.RemoveListener(OnSaveButtonClicked);
            loadButton.onClick.RemoveListener(OnLoadButtonClicked); 
            optionsButton.onClick.RemoveListener(OnOptionsButtonClicked);
            exitButton.onClick.RemoveListener(OnExitButtonClicked);
            backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
        
        private void Update()
        {
            // Check if Esc key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Esc Pressed");
                //If option menu is already open, close it, else oprn it
                if (pauseMenuPanel.activeSelf)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            }
        }

        //  Method for Resume button clicked
        private void OnResumeButtonClicked()
        {
            Debug.Log("Resume Button Clicked");
        }

        private void OnSaveButtonClicked()
        {
            GameSaveManager.SaveGame();
            Debug.Log("Save Button Clicked");
            Hide();
        }

        private void OnLoadButtonClicked()
        {
            GameSaveManager.LoadGame();
            Debug.Log("Load Button Clicked");
            Hide();
        }

        // Method for Options button clicked
        private void OnOptionsButtonClicked()
        {
            UiEventManager.OpenOptionMenu();
            Debug.Log("Pause Menu Options Button Clicked");
        }

        // Method for Exit button clicked
        private void OnExitButtonClicked()
        {
            Debug.Log("Pause Menu Exit Button Clicked");
            Application.Quit();
        }

        // Method for back button clicked
        private void OnBackButtonClicked()
        {
            Hide();
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
