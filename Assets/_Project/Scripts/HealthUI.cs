using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
            characterLifeController.OnHealed += OnLifeChanged;
            OnLifeChanged(characterLifeController.health);
        }

        private void OnDestroy ()
        {
            characterLifeController.OnDamageRecieved -= OnLifeChanged;
            characterLifeController.OnHealed -= OnLifeChanged;
        }

        private void OnLifeChanged (int _)
        {
            slider.DOValue(characterLifeController.health/100f, 0.2f).SetEase(Ease.OutQuad);
        }
    }
}
