using UnityEngine;

namespace Surviblewilderness
{
    public class EnemyBasicAttack : MonoBehaviour
    {
        private const float COLD_DOWN = 2;

        [SerializeField] private int attackDamage = 10;
        [SerializeField] private AudioClip attackSound;

        private float currentColdDownCounter;

        private IDamageable target;

        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = other.GetComponent<IDamageable>();
            }
        }

        private void OnTriggerExit (Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = null;

            }
        }

        private void Update ()
        {
            UpdateTarget();
        }

        private void UpdateTarget ()
        {
            if (target == null)
                return;
            if (currentColdDownCounter>0)
                currentColdDownCounter -=Time.deltaTime;
            else
            {
                currentColdDownCounter = COLD_DOWN;
                target.ApplyDamage(attackDamage);
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
            }
        }
    }
}
