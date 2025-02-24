using UnityEngine;

namespace Surviblewilderness
{
    public class DamageSoundManager : MonoBehaviour
    {
        [SerializeField] private IDamageable damageable;
        //[SerializeField] private IAttacker attacker;
        //[SerializeField] private IDestructible destructible;

        //[SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip damageSound;
        //[SerializeField] private AudioClip attackSound;
        //[SerializeField] private AudioClip deathSound;

        private void Awake()
        {
            damageable = GetComponentInParent<IDamageable>();
            //audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            damageable.OnDamageRecieved += OnDamageRecieved;

            //destructible.OnDestroyed += OnDestoy;
        }

        private void OnDisable()
        {
            damageable.OnDamageRecieved -= OnDamageRecieved;

            //destructible.OnDestroyed -= OnDestoy;
        }

        private void OnDamageRecieved(int _)
        {
            //reproduce sound of everithin that you want
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }

        //private void OnAttack(int _)
        //{
        //    //reproduce sound of everithin that you want
        //    AudioSource.PlayClipAtPoint(attackSound, transform.position);
        //}

        //private void OnDestoy() 
        //{ 
        //    AudioSource.PlayClipAtPoint(deathSound, transform.position);
        //}
    }
}
