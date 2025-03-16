using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class CharacterLifeController : MonoBehaviour, IDamageable, IHealable
    {
        private const int MAX_HEALTH = 100;

        public event Action<int> OnDamageRecieved;
        public event Action<int> OnHealed;

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


        public void Heal (int amount)
        {
            _health += amount;
            if (_health > MAX_HEALTH)
                _health = MAX_HEALTH;
            OnHealed?.Invoke(amount);
        }
    }
}
