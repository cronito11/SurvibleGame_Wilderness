using System;
using UnityEngine;

public class DestructibleProps : MonoBehaviour, IDamageable, IDestructible
{
    public event Action OnDestroyed;
    public event Action<int> OnDamageRecieved;

    [SerializeField] private int health = 100;


    public void ApplyDamage (int amount)
    {
        if (health == 0)
            return;

        health -= amount;
        OnDamageRecieved?.Invoke (amount);
        if (health <= 0)
        { 
            health = 0;
            Destroy ();
        }
    }

    public void Destroy ()
    {
        OnDestroyed();
    }
}
