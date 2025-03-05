using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class GamePlayMenuUiManager : MonoBehaviour, IUiPannel
    {
        // UI Components for Gameplay
        [Header("Gameplay Panel Components")]
        [SerializeField] private GameObject gamePlayPanel; // Gameplay UI Panel
        [SerializeField] private Button pauseButton; // Pause Button
        [SerializeField] private Button inventoryButton; // Inventory Button
        [SerializeField] private Slider healthSlider; // Health Bar Slider
        [SerializeField] private Slider forceSlider; // Force Bar Slider
        [SerializeField] private GameObject miniMap; // Mini-Map
        [SerializeField] private TextMeshProUGUI timeText; // In-Game Time Display

        private void OnEnable()
        {
            // Subscribe to gameplay menu events
            UiEventManager.OnGamePlayMenuOpened += Show;
            UiEventManager.OnGamePlayMenuClosed += Hide;

            // Attaching Button Listeners
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
            inventoryButton.onClick.AddListener(OnInventoryButtonClicked);
        }

        private void OnDisable()
        {
            // Unsubscribe from gameplay menu events
            UiEventManager.OnGamePlayMenuOpened -= Show;
            UiEventManager.OnGamePlayMenuClosed -= Hide;

            // Removing Button Listeners
            pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            inventoryButton.onClick.RemoveListener(OnInventoryButtonClicked);
        }

        // Method for Pause button clicked
        private void OnPauseButtonClicked()
        {
            Debug.Log("Pause Button Clicked");
        }

        // Method for Inventory button clicked
        private void OnInventoryButtonClicked()
        {
            Debug.Log("Inventory Button Clicked");
        }

        // Update the health slider value
        public void UpdateHealthSlider(float health)
        {
            healthSlider.value = health;
        }

        // Update the force slider value
        public void UpdateForceSlider(float force)
        {
            forceSlider.value = force;
        }

        // Update the in-game time display
        public void UpdateTimeDisplay(string time)
        {
            timeText.text = time;
        }

        // Show Panel
        public void Show()
        {
            gamePlayPanel.SetActive(true);
        }

        // Hide Panel
        public void Hide()
        {
            gamePlayPanel.SetActive(false);
        }

        // Check if panel is active
        public bool IsActive()
        {
            return gamePlayPanel.activeSelf;
        }
    }
}