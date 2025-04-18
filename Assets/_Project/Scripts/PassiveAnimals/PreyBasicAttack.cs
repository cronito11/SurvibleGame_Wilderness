using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class PreyBasicAttack : MonoBehaviour, IAttacker
    {
        // Attack trigger animation cache
        private static readonly int AttackAnim = Animator.StringToHash("Attack");
        
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
            if (!other.CompareTag("Player")) return;
            
            currentTarget = other.GetComponent<IDamageable>();
            currentTargetTag = other.tag;
            Attack(); // Use the interface method
        }

        private void OnTriggerStay(Collider other)
        {
            if ((other.CompareTag("Player")) 
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
                anim.SetTrigger(AttackAnim);
            }

            currentTarget.ApplyDamage(attackDamage);
            OnAttack?.Invoke();
            currentCooldown = COOL_DOWN;
        }
    }
}
