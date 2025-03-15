using System;
using UnityEngine;

public class DestructibleProps : MonoBehaviour, IDamageable, IDestructible, IHealable
{
    private const int MAX_HEALTH = 100;

    public event Action OnDestroyed;
    public event Action<int> OnDamageRecieved;
    public event Action<int> OnHealed;
    
    [SerializeField] private int _health = MAX_HEALTH;

    public int health => _health;

    public void Heal (int amount)
    {
        _health += amount;
        if(_health > MAX_HEALTH)
            _health = MAX_HEALTH;
        OnHealed?.Invoke(amount);
    }

    public void ApplyDamage (int amount)
    {
        if (_health == 0)
            return;

        _health -= amount;
        OnDamageRecieved?.Invoke (amount);
        if (_health <= 0)
        { 
            _health = 0;
            Destroy ();
        }
    }

    public void Destroy ()
    {
        OnDestroyed();
    }
}
