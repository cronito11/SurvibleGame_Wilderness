using System;
using Unity.VisualScripting;
using UnityEngine;

public class ParticlesDestructibleProps : MonoBehaviour
{
    private IDamageable damageable;
    private IDestructible destructible;

    [SerializeField] private ParticleSystem damage_Particle;
    [SerializeField] private ParticleSystem destroy_Particle;

    private void Awake ()
    {
        damageable = GetComponentInParent<IDamageable>();
        destructible = GetComponentInParent<IDestructible>();
    }

    private void OnEnable ()
    {
        damageable.OnDamageRecieved += OnDamageRecieved;
        destructible.OnDestroyed += OnDestroyed;
    }

    private void OnDisable ()
    {
        destructible.OnDestroyed -= OnDestroyed;
        damageable.OnDamageRecieved += OnDamageRecieved;
    }

    private void OnDestroyed ()
    {
        destroy_Particle.Play();
    }

    private void OnDamageRecieved (int _)
    {
        damage_Particle.Play();
    }
}
