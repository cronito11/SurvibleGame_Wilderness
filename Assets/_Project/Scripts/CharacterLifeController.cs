using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class CharacterLifeController : MonoBehaviour, IDamageable
    {
        public event Action<int> OnDamageRecieved;

        [SerializeField] private int _health = 100;

        public int health => _health;

        public void ApplyDamage (int amount)
        {
            if (health <=0)
                return;
            _health -= amount;
            OnDamageRecieved?.Invoke(amount);

            if (_health <=0)
                _health = 0;
        }
    }
}
