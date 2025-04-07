using UnityEngine;

namespace Surviblewilderness
{
    public class DestroyPassiveAnimalDeath : MonoBehaviour
    {
        [SerializeField] private IDamageable damageable;
        private PassiveAnimalSpawner passiveAnimalSpawner;

        private void Awake ()
        {
            damageable = GetComponentInParent<IDamageable>();
            passiveAnimalSpawner = GetComponent<PassiveAnimalSpawner>();
        }

        private void OnEnable ()
        {
            damageable.OnDamageRecieved += OnDamageRecieved;
        }

        private void OnDisable ()
        {
            damageable.OnDamageRecieved -= OnDamageRecieved;
        }

        private void OnDamageRecieved (int _)
        {
            if (damageable.health != 0)
                return;
            passiveAnimalSpawner.RemovePassiveAnimal(GetComponentInParent<PassiveAnimalManager>());
            Destroy(gameObject);
        }
    }
}
