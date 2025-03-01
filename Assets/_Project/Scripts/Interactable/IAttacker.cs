using System;

namespace Surviblewilderness
{
    public interface IAttacker 
    {
        event Action OnAttack;
        public void Attack ();
    }
}
