using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class CharacterLifeController : MonoBehaviour, IDamageable, IHealable
    {
        private static readonly int Hit = Animator.StringToHash("Hit");

        private const int MAX_HEALTH = 100;
        public event Action<int> OnDamageRecieved;
        public event Action<int> OnHealed;

        [SerializeField] private Animator[] _animator;
        [SerializeField] private int _health = 100;


        public int health => _health;
        [Header("Player")]
        public bool isPlayer;
        [SerializeField] private int  healthReduction = 2;
        [SerializeField] private float reduceHealthTime = 5f;
        private float currentTime = 0;

        private void OnEnable()
        {
            InventoryItem.OnConsumeFoodItem += ReStoreHealthOnFoodConsume;
        }
        private void OnDisable()
        {
            InventoryItem.OnConsumeFoodItem -= ReStoreHealthOnFoodConsume;
        }


        public void ApplyDamage (int amount)
        {
            if (health <=0)
                return;
            for (int idx = 0; idx < _animator.Length; idx++)
            {
                _animator [idx].SetTrigger(Hit);
            }
            _health -= amount;
            OnDamageRecieved?.Invoke(amount);

            if (_health <=0)
                _health = 0;
        }

        private void Update ()
        {
            if (!isPlayer)
                return;
            if (currentTime > 0)
                currentTime -= Time.deltaTime;
            else
            {
                currentTime = reduceHealthTime;
                ApplyDamage(healthReduction);
            }
        }


        public void Heal (int amount)
        {
            _health += amount;
            if (_health > MAX_HEALTH)
                _health = MAX_HEALTH;
            OnHealed?.Invoke(amount);
            Debug.Log($"Healed {amount} health, Current Health: {_health}");
        }

        public void ReloadHealth(int health)
        {
            _health = health;
        }

        private void ReStoreHealthOnFoodConsume(GameItemSO gameItemSO)
        {
            FoodItemSO foodItem = gameItemSO as FoodItemSO;
            Debug.Log($"Restoring health with {foodItem.itemName}, Restores: {foodItem.staminaReplenishment} Hunger");
            Heal(foodItem.staminaReplenishment);
        }
    }
}
