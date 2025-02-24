using System;

namespace Surviblewilderness
{
    public interface IPlayerLifeController
    {
        int health { get; }

        event Action<int> OnDamageRecieved;

        void ApplyDamage (int amount);
    }
}