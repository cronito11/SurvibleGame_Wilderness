using System;
using UnityEngine;

public class DestructibleProps : MonoBehaviour, IDamageable, IDestructible
{
    public event Action OnDestroyed;
    public event Action<int> OnDamageRecieved;

    [SerializeField] private int _health = 100;

    public int health => _health;

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
