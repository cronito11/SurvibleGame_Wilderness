using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class PreyBasicAttack : MonoBehaviour, IAttacker
    {
        private const float COLD_DOWN = 2;

        [SerializeField] private int attackDamage = 10;
        //[SerializeField] private AudioClip attackSound;

        private float currentColdDownCounter;

        private IDamageable target;
        private IDamageable player;

        public event Action OnAttack;



        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player = other.GetComponent<IDamageable>();
            }
        }

        private void OnTriggerExit (Collider other)
        {
            if (other.CompareTag("Player"))
            {
                player = null;
            }
        }

        private void Update ()
        {
            Attack();
        }

        private void UpdateTarget ()
        {
            if (target == null && player == null)
                return;
            if (currentColdDownCounter>0)
                currentColdDownCounter -=Time.deltaTime;
            else
            {
                currentColdDownCounter = COLD_DOWN;
                if (player != null)
                    player.ApplyDamage(attackDamage);
                else if (target != null)
                    target.ApplyDamage(attackDamage);
                //AudioSource.PlayClipAtPoint(attackSound, transform.position);
            }
        }

        public void Attack ()
        {

            if (target == null && player == null)
                return;
            if (currentColdDownCounter > 0)
                currentColdDownCounter -= Time.deltaTime;
            else
            {
                currentColdDownCounter = COLD_DOWN;
                if (player != null)
                    player.ApplyDamage(attackDamage);
                else if (target != null)
                    target.ApplyDamage(attackDamage);
                OnAttack?.Invoke();
            }
        }
    }
}
