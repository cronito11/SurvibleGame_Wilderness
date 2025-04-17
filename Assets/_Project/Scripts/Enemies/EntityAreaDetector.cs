using System;
using UnityEngine;

namespace Surviblewilderness
{
    public class EntityAreaDetector : MonoBehaviour
    {
        private EntityManager<IObserver> manager;

        private void Awake ()
        {
            manager = GetComponentInParent<EntityManager<IObserver>>();
        }

        private void OnTriggerEnter (Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                manager.FindTarget(other.transform);
            }
        }

        private void OnTriggerExit (Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                manager.FindTarget(other.transform);
            }
        }
    }
}
