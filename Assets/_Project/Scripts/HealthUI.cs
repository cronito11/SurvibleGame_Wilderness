using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class HealthUI : MonoBehaviour
    {
        private Slider slider;
        [SerializeField] private CharacterLifeController characterLifeController;

        private void Awake ()
        {
            if(characterLifeController == null) 
                characterLifeController  = GetComponentInParent<CharacterLifeController>();
            slider = GetComponent<Slider>();
        }
        private void Start ()
        {
            characterLifeController.OnDamageRecieved += OnLifeChanged;
            OnLifeChanged(characterLifeController.health);
        }

        private void OnDestroy ()
        {
            characterLifeController.OnDamageRecieved -= OnLifeChanged;
        }

        private void OnLifeChanged (int obj)
        {
            slider.value = characterLifeController.health/100f;
        }
    }
}
