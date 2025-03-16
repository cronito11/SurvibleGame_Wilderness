using UnityEngine;
using UnityEngine.UI;

namespace Surviblewilderness
{
    public class HealthPropsUI : MonoBehaviour
    {
        private Slider slider;
        private IDamageable destructible;
        private IHealable healable;

        private void Awake ()
        {
            destructible  = GetComponentInParent<IDamageable>();
            healable = GetComponentInParent<IHealable>();
            slider = GetComponent<Slider>();
        }
        private void Start ()
        {
            destructible.OnDamageRecieved += OnLifeChanged;
            healable.OnHealed += OnLifeChanged;
            OnLifeChanged(healable.health);
        }

        private void OnDestroy ()
        {
            destructible.OnDamageRecieved -= OnLifeChanged;
            healable.OnHealed -= OnLifeChanged;

        }

        private void OnLifeChanged (int _)
        {
            slider.value = healable.health/100f;
        }
    }
}
