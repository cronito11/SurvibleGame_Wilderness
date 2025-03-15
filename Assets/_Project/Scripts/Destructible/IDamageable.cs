using System;
using UnityEngine;

public interface IDamageable
{
    event Action<int> OnDamageRecieved;
    public int health { get; }
    public void ApplyDamage (int amount);
}

public interface IDestructible
{
    event Action OnDestroyed;

    public void Destroy();
}

public interface IHealable
{
    event Action<int> OnHealed;
    public int health { get; }
    public void Heal (int amount);
}
