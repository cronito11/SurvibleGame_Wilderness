using UnityEngine;

namespace Surviblewilderness
{
    public class DestroyOnDay : MonoBehaviour
    {
        private void OnEnable ()
        {
            
            TimeController.OnChangeTimeOfDay += ManageSpawningSystem;
        }

        private void ManageSpawningSystem (TimeOfDay day)
        {
            if (TimeOfDay.Day == day)
            {
                IDamageable damageable = GetComponentInParent<IDamageable>();
                damageable.ApplyDamage(damageable.health);
            }
        }

        private void OnDisable ()
        {
            TimeController.OnChangeTimeOfDay -= ManageSpawningSystem;
        }
    }
}
