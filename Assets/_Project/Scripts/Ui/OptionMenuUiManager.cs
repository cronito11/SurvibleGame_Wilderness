using UnityEngine;
using UnityEngine.UI;

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

        private void Update()
        {
            // Check if Esc key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Esc Pressed");
                //If option menu is already open, close it, else oprn it
                if (optionMenuPannel.activeSelf)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
            }
        }

        //  Mehtod for Back button clicked
        private void OnBackButtonClicked()
        {
            Hide();
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
