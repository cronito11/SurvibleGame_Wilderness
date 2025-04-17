using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class DestroyCharacterDeath : MonoBehaviour
    {
        [SerializeField] private IDamageable damageable;

        private void Awake ()
        {
            damageable = GetComponentInParent<IDamageable>();
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
            Destroy(gameObject);
        }
    }

    public class DestroyCharacterFadeDeath : MonoBehaviour
    {
        [SerializeField] private IDamageable damageable;

        private void Awake ()
        {
            damageable = GetComponentInParent<IDamageable>();
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
            //Make the fade
        }
    }
}
