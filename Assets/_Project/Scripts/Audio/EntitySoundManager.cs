using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class EntitySoundManager : MonoBehaviour
    {
        [SerializeField] private IDamageable damageable;
        [SerializeField] private IAttacker attacker;
        [SerializeField] private IDestructible destructible;

        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private AudioClip damageSound;
        [SerializeField] private AudioClip attackSound;
        [SerializeField] private AudioClip destroySound;

        private void Awake()
        {
            damageable = GetComponent<IDamageable>();
            attacker = GetComponent<IAttacker>();
            destructible = GetComponent<IDestructible>();

            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if(damageable != null)
                damageable.OnDamageRecieved += OnDamageRecieved;
            if(attacker != null)
                attacker.OnAttack += OnAttack;
            if(destructible != null)
                destructible.OnDestroyed += OnDestoy;
            
        }

        private void OnDisable()
        {
            if(damageable != null)
                damageable.OnDamageRecieved -= OnDamageRecieved;
            if (attacker != null) 
                attacker.OnAttack -= OnAttack;
            if (destructible != null) 
                destructible.OnDestroyed -= OnDestoy;
        }

        private void OnDestoy()
        {
            if (destroySound is null)
            {
                Debug.Log("Sound clip is null either this sound is not relevent to this entity or it is not assigned");
                return;
            }
                
            audioSource.PlayOneShot(destroySound);
        }

        private void OnAttack()
        {
            if (attackSound is null)
            {
                Debug.Log("Sound clip is null either this sound is not relevent to this entity or it is not assigned");
                return;
            }
            audioSource.PlayOneShot(attackSound);
        }

        private void OnDamageRecieved(int _)
        {
            if (damageSound is null)
            {
                Debug.Log("Sound clip is null either this sound is not relevent to this entity or it is not assigned");
                return;
            }
            audioSource.PlayOneShot(damageSound);   
        }
    }
}
