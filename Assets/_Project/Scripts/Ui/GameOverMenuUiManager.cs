using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Surviblewilderness
{
    public class GameOverMenuUiManager : MonoBehaviour, IUiPannel
    {
        // UI Components for Game Over Screen
        [Header("Game Over Panel Components")]
        [SerializeField] private GameObject gameOverMenuPannel; // Game Over Menu Panel
        [SerializeField] private Button backToMainMenuButton; // Return to Main Menu Button
        [SerializeField] private Button exitGameButton; // Exit Game Button

        private void OnEnable()
        {
            // Subscribe to game over menu events
            UiEventManager.OnGameOverMenuOpened += Show;
            UiEventManager.OnGameOverMenuClosed += Hide;

            // Attach button listeners
            backToMainMenuButton.onClick.AddListener(OnBackToMainMenuButtonClicked);
            exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        }

        private void OnDisable()
        {
            // Unsubscribe from game over menu events
            UiEventManager.OnGameOverMenuOpened -= Show;
            UiEventManager.OnGameOverMenuClosed -= Hide;

            // Remove button listeners
            backToMainMenuButton.onClick.RemoveListener(OnBackToMainMenuButtonClicked);
            exitGameButton.onClick.RemoveListener(OnExitGameButtonClicked);
        }

        // Method for Back to Main Menu button clicked
        private void OnBackToMainMenuButtonClicked()
        {
            Debug.Log("Back to Main Menu Button Clicked");
            SceneManager.LoadScene("Menu");

        }

        // Method for Exit Game button clicked
        private void OnExitGameButtonClicked()
        {
            Debug.Log("Exit Game Button Clicked");
        }

        // Show Panel
        public void Show()
        {
            gameOverMenuPannel.SetActive(true);
        }

        // Hide Panel
        public void Hide()
        {
            gameOverMenuPannel.SetActive(false);
        }

        // Check if panel is active
        public bool IsActive()
        {
            return gameOverMenuPannel.activeSelf;
        }
    }
}