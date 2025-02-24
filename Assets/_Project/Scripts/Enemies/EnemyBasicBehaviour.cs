using System;
using UnityEngine;
using UnityEngine.AI;

namespace Surviblewilderness
{
    public class EnemyBasicBehaviour : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Transform target;

        private void Awake ()
        {
            agent = GetComponentInParent<NavMeshAgent> ();
        }
        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag("Player"))
            {
                target = other.transform;
            }
        }

        private void Update ()
        {
            UpdateTarget();
        }

        private void UpdateTarget ()
        {
            if (!target)
                return;
            agent.destination = target.position;
        }
    }
}
