using System;
using Surviblewilderness;
using UnityEngine;

namespace Surviblewildness
{
    public class EnemyBasicAttack : MonoBehaviour, IAttacker
    {
        // Attack trigger animation cache
        private static readonly int Attack1 = Animator.StringToHash("Attack1");
        private static readonly int Attack5 = Animator.StringToHash("Attack5");
        
        private const float COOL_DOWN = 2.0f;

        [SerializeField] private int attackDamage = 10;
        [SerializeField] private Animator anim;

        private float currentCooldown;
        private IDamageable currentTarget;
        private string currentTargetTag;

        public event Action OnAttack;

        // Explicit interface implementation
        public void Attack()
        {
            // This is now just a public wrapper for TryAttack
            TryAttack();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Prey")) return;
            
            currentTarget = other.GetComponent<IDamageable>();
            currentTargetTag = other.tag;
            Attack(); // Use the interface method
        }

        private void OnTriggerStay(Collider other)
        {
            if ((other.CompareTag("Player") || other.CompareTag("Prey")) 
                && other.GetComponent<IDamageable>() == currentTarget)
            {
                Attack(); // Use the interface method
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IDamageable>() == currentTarget)
            {
                currentTarget = null;
                currentTargetTag = null;
            }
        }

        private void TryAttack()
        {
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
                return;
            }

            if (currentTarget == null || anim == null) return;

            // Trigger appropriate animation
            if (currentTargetTag == "Player")
            {
                anim.SetTrigger(Attack1);
            }
            else if (currentTargetTag == "Prey")
            {
                anim.SetTrigger(Attack5);
            }

            currentTarget.ApplyDamage(attackDamage);
            OnAttack?.Invoke();
            currentCooldown = COOL_DOWN;
        }
    }
}