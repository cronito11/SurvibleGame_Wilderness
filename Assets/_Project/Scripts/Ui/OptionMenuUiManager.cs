using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Surviblewilderness
{
    public class OptionMenuUiManager : MonoBehaviour, IUiPannel
    {
        // UI Components for the Options Menu
        [Header("Option Menu Panel Components")]
        [SerializeField] private GameObject optionMenuPannel;
        [SerializeField] private Button backButton; // Back Butto
        [SerializeField] private Slider musicSoundSlider; // Music Volume Slider
        [SerializeField] private Slider sfxSoundSlider; // SFX Volume Slider
        [SerializeField] private Button controllsButton; // View Controls Button

        private void OnEnable()
        {
            UiEventManager.OnOptionMenuOpened += Show;
            UiEventManager.OnOptionMenuClosed += Hide;

            // Attaching Button Listners
            backButton.onClick.AddListener(OnBackButtonClicked);
            musicSoundSlider.onValueChanged.AddListener(OnMusicSoundSliderDrag);
            sfxSoundSlider.onValueChanged.AddListener(OnSFXSoundSliderDrag);
            controllsButton.onClick.AddListener(OnControllsButtonClicked);
        }


        private void OnDisable()
        {
            UiEventManager.OnOptionMenuOpened -= Show;
            UiEventManager.OnOptionMenuClosed -= Hide;

            // Removing Button Listners
            backButton.onClick.RemoveListener(OnBackButtonClicked);
            musicSoundSlider.onValueChanged.RemoveListener(OnMusicSoundSliderDrag);
            sfxSoundSlider.onValueChanged.RemoveListener(OnSFXSoundSliderDrag);
            controllsButton.onClick.RemoveListener(OnControllsButtonClicked);
        }

        //  Mehtod for Back button clicked
        private void OnBackButtonClicked()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) // Build Index "0" = Menu Scene
            {
                UiEventManager.OpenMainMenu();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1) // Build Index "1" = Level Secene
            {
                UiEventManager.OpenPauseMenu();
            }
            Debug.Log("Back Button Clicked");
        }

        //  Mehtod for Music Slider drag
        private void OnMusicSoundSliderDrag(float value)
        {
            Debug.Log($"Music Slider Value Changed: {value}");
        }

        //  Mehtod for SFX Sound drag
        private void OnSFXSoundSliderDrag(float value)
        {
            Debug.Log($"SFX Sound Slider Value Changed: {value}");
        }

        //  Mehtod for Controlls button clicked
        private void OnControllsButtonClicked()
        {
            UiEventManager.OnButtonClick?.Invoke();
            Debug.Log("Contorlls Button Clicked");
        }

        // Show Panel
        public void Show()
        {
            optionMenuPannel.SetActive(true);
        }
        // Hide Panel
        public void Hide()
        {
            optionMenuPannel.SetActive(false);
        }
        // Check if panel is active
        public bool IsActive()
        {
            return optionMenuPannel.activeSelf;
        }
    }
}
