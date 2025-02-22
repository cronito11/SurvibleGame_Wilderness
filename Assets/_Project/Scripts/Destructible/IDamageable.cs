using System;
using UnityEngine;

public interface IDamageable
{
    event Action<int> OnDamageRecieved;

    public void ApplyDamage (int amount);
}

public interface IDestructible
{
    event Action OnDestroyed;

    public void Destroy();
}
